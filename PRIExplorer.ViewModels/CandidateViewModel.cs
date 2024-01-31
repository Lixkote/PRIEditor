using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using PriFormat;

namespace PRIExplorer.ViewModels;

public class CandidateViewModel
{
	private PriFile priFile;

	private Stream priStream;

	private string resourceRootPath;

	private ResourceMapItem resourceMapItem;

	private IReadOnlyList<Qualifier> qualifiers;

	private string locationPath;

	public Candidate Candidate { get; }

	public string QualifiersDescription
	{
		get
		{
			if (qualifiers.Count == 0)
			{
				return "(none)";
			}
			return string.Join(", ", qualifiers.Select((Qualifier q) => q.Type.ToString() + "=" + q.Value));
		}
	}

	private string FullLocationPath
	{
		get
		{
			string path = (Candidate.SourceFile.HasValue ? Path.GetDirectoryName(priFile.GetReferencedFileByRef(Candidate.SourceFile.Value).FullName) : resourceRootPath);
			return Path.Combine(path, locationPath);
		}
	}

	public bool SourceNotFound { get; private set; }

	public bool LocationNotFound { get; private set; }

	public string Location { get; }

	public RelayCommand GoToLocationCommand { get; }

	public RelayCommand SaveAsCommand { get; }

    public CandidateViewModel(PriFile priFile, Stream priStream, string resourceRootPath, ResourceMapItem resourceMapItem, Candidate candidate)
	{
		GoToLocationCommand = new RelayCommand(GoToLocationCommand_CanExecute, GoToLocationCommand_Execute);
		SaveAsCommand = new RelayCommand(SaveAsCommand_CanExecute, SaveAsCommand_Execute);
        this.priFile = priFile;
		this.priStream = priStream;
		this.resourceRootPath = resourceRootPath;
		this.resourceMapItem = resourceMapItem;
		Candidate = candidate;
		DecisionInfoSection sectionByRef = priFile.GetSectionByRef(priFile.PriDescriptorSection.DecisionInfoSections.First());
		qualifiers = sectionByRef.QualifierSets[candidate.QualifierSet].Qualifiers;
		if (candidate.Type == ResourceValueType.AsciiPath || candidate.Type == ResourceValueType.Utf8Path || candidate.Type == ResourceValueType.Path)
		{
			string text = (string)GetData();
			if (text != null)
			{
				locationPath = text;
				Location = text;
				if (!File.Exists(FullLocationPath))
				{
					LocationNotFound = true;
				}
			}
			else
			{
				Location = "";
			}
		}
		else
		{
			Location = "(embedded)";
		}
		if (candidate.SourceFile.HasValue)
		{
			string fullName = priFile.GetReferencedFileByRef(candidate.SourceFile.Value).FullName;
			if (File.Exists(fullName))
			{
				Location = fullName + ": " + Location;
				return;
			}
			Location = fullName + " (not found)";
			SourceNotFound = true;
		}
	}

	public object GetData()
	{
		byte[] array;
		if (!Candidate.SourceFile.HasValue)
		{
			ByteSpan byteSpan = ((!Candidate.DataItem.HasValue) ? Candidate.Data.Value : this.priFile.GetDataItemByRef(Candidate.DataItem.Value));
			priStream.Seek(byteSpan.Offset, SeekOrigin.Begin);
			using BinaryReader binaryReader = new BinaryReader(priStream, Encoding.Default, leaveOpen: true);
			array = binaryReader.ReadBytes((int)byteSpan.Length);
		}
		else
		{
			string fullName = this.priFile.GetReferencedFileByRef(Candidate.SourceFile.Value).FullName;
			if (!File.Exists(fullName))
			{
				return null;
			}
			using FileStream fileStream = File.OpenRead(fullName);
			PriFile priFile = PriFile.Parse(fileStream);
			ByteSpan dataItemByRef = priFile.GetDataItemByRef(Candidate.DataItem.Value);
			fileStream.Seek(dataItemByRef.Offset, SeekOrigin.Begin);
			using BinaryReader binaryReader2 = new BinaryReader(fileStream, Encoding.Default, leaveOpen: true);
			array = binaryReader2.ReadBytes((int)dataItemByRef.Length);
		}
		switch (Candidate.Type)
		{
		case ResourceValueType.AsciiString:
		case ResourceValueType.AsciiPath:
			return Encoding.ASCII.GetString(array).TrimEnd(default(char));
		case ResourceValueType.Utf8String:
		case ResourceValueType.Utf8Path:
			return Encoding.UTF8.GetString(array).TrimEnd(default(char));
		case ResourceValueType.String:
		case ResourceValueType.Path:
			return Encoding.Unicode.GetString(array).TrimEnd(default(char));
		case ResourceValueType.EmbeddedData:
			return array;
		default:
			throw new Exception();
		}
	}

	private bool GoToLocationCommand_CanExecute()
	{
		return locationPath != null;
	}

	private void GoToLocationCommand_Execute()
	{
		string fullLocationPath = FullLocationPath;
		Process.Start("explorer.exe", $"/select,\"{fullLocationPath}\"");
	}

	private bool SaveAsCommand_CanExecute()
	{
		return true;
	}

	private void SaveAsCommand_Execute()
	{
		object data = GetData();
		if (data == null)
		{
			return;
		}
		byte[] bytes = null;
		string text = null;
		switch (Candidate.Type)
		{
		case ResourceValueType.String:
		case ResourceValueType.AsciiString:
		case ResourceValueType.Utf8String:
			bytes = Encoding.UTF8.GetBytes((string)data);
			break;
		case ResourceValueType.Path:
		case ResourceValueType.AsciiPath:
		case ResourceValueType.Utf8Path:
		{
			string path = (Candidate.SourceFile.HasValue ? Path.GetDirectoryName(priFile.GetReferencedFileByRef(Candidate.SourceFile.Value).FullName) : resourceRootPath);
			text = Path.Combine(path, (string)data);
			break;
		}
		case ResourceValueType.EmbeddedData:
			bytes = (byte[])data;
			break;
		}
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "All files (*.*)|*.*";
		if (text != null)
		{
			saveFileDialog.FileName = Path.GetFileName(text);
		}
		else if (qualifiers.Any())
		{
			saveFileDialog.FileName = Path.GetFileNameWithoutExtension(resourceMapItem.Name) + "." + string.Join("_", qualifiers.Select((Qualifier q) => q.Type.ToString().ToLower() + "-" + q.Value.ToLower())) + Path.GetExtension(resourceMapItem.Name);
		}
		else
		{
			saveFileDialog.FileName = resourceMapItem.Name;
		}
		if (saveFileDialog.ShowDialog() == true)
		{
			if (text != null)
			{
				File.Copy(text, saveFileDialog.FileName, overwrite: true);
			}
			else
			{
				File.WriteAllBytes(saveFileDialog.FileName, bytes);
			}
		}
	}
}
