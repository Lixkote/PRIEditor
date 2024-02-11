using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using PRIExplorer.Views;
using PriFormat;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;

namespace PRIExplorer.ViewModels;

public class MainViewModel : INotifyPropertyChanged
{
	private object previewContent;

	private EntryViewModel selectedEntry;

	private CandidateViewModel selectedCandidate;

    public RelayCommand SaveXbfCommand { get; }

    public FileStream PriStream { get; private set; }

    public FileStream ModdedPriStream { get; private set; }

    public PriFile DePriFile { get; private set; }

	public string ResourceRootPath { get; private set; }

	public ObservableCollection<EntryViewModel> Entries { get; private set; }

	public ObservableCollection<CandidateViewModel> Candidates { get; private set; }

	public RelayCommand OpenCommand { get; }

	public RelayCommand CloseCommand { get; }

    public RelayCommand SaveAsPriCommand { get; }

    public RelayCommand SetResourceRootPathCommand { get; }

	public EntryViewModel SelectedEntry
	{
		get
		{
			return selectedEntry;
		}
		set
		{
			if (selectedEntry != value)
			{
				selectedEntry = value;
				SelectedEntryChanged();
			}
		}
	}

	public CandidateViewModel SelectedCandidate
	{
		get
		{
			return selectedCandidate;
		}
		set
		{
			if (selectedCandidate != value)
			{
				selectedCandidate = value;
				SelectedCandidateChanged();
			}
		}
	}

	public object PreviewContent
	{
		get
		{
			return previewContent;
		}
		set
		{
			if (previewContent != value)
			{
				previewContent = value;
				this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PreviewContent"));
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public MainViewModel()
	{
		OpenCommand = new RelayCommand(OpenCommand_Execute);
        SaveAsPriCommand = new RelayCommand(SaveAsPriCommand_Execute);
        SaveXbfCommand = new RelayCommand(SaveXbfCommand_Execute);
        CloseCommand = new RelayCommand(CloseCommand_Execute);
		SetResourceRootPathCommand = new RelayCommand(SetResourceRootPathCommand_CanExecute, SetResourceRootPathCommand_Execute);
		Entries = new ObservableCollection<EntryViewModel>();
        Candidates = new ObservableCollection<CandidateViewModel>();
	}

    private void CloseCommand_Execute()
	{
		Application.Current.Shutdown();
	}

	private bool SetResourceRootPathCommand_CanExecute()
	{
		return DePriFile != null;
	}

	private void SetResourceRootPathCommand_Execute()
	{
		CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
		commonOpenFileDialog.IsFolderPicker = true;
		commonOpenFileDialog.Title = "Set resource path root";
		commonOpenFileDialog.Multiselect = false;
		commonOpenFileDialog.InitialDirectory = ResourceRootPath;
		if (commonOpenFileDialog.ShowDialog() == CommonFileDialogResult.Ok)
		{
			ResourceRootPath = commonOpenFileDialog.FileName;
			GetEntries();
		}
	}

    // Add this method to MainViewModel class
    private void SaveAsPriCommand_Execute()
    {
		SaveModifiedPriFile();
    }

    private int GetCandidateIndex(ResourceMapItem resourceMapItem, Candidate candidate)
    {
        ResourceMapSection primaryResourceMapSection = DePriFile.GetSectionByRef(DePriFile.PriDescriptorSection.PrimaryResourceMapSection.Value);

        if (primaryResourceMapSection.CandidateSets.TryGetValue(resourceMapItem.Index, out var candidateSet))
        {
            for (int i = 0; i < candidateSet.Candidates.Count; i++)
            {
                if (candidateSet.Candidates[i] == candidate)
                {
                    return i;
                }
            }
        }

        return -1; // Candidate not found
    }  



    private bool SaveXbfCommand_CanExecute()
    {
        // Add your conditions for when the Save XBF command can execute
        return PreviewContent is XbfPreviewPage;
    }

    // Modify the SavePriFile method as follows
    public void SavePriFile(string path)
    {
        try
        {
            // Use File.Copy to copy the existing PRI file to the new location
            File.Copy(PriStream.Name, path, true);

            // Close the existing PRI file
            ClosePriFile();

            // Open the PRI file from the new location
            PriStream = File.OpenRead(path);
            DePriFile = PriFile.Parse(PriStream);

            // Update the ResourceRootPath and reload entries
            ResourceRootPath = Path.GetDirectoryName(path);
            GetEntries();
            SetResourceRootPathCommand.RaiseCanExecuteChanged();
        }
        catch
        {
            ClosePriFile();
            MessageBox.Show("Could not save file.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            return;
        }
    }


    public void OpenPriFile(string path)
    {
        try
        {
            PriStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite);
            DePriFile = PriFile.Parse(PriStream);
        }
        catch (Exception ex)
        {
            ClosePriFile();
            MessageBox.Show(ex.ToString(), "Could not read file.", MessageBoxButton.OK, MessageBoxImage.Question);
            return;
        }

        if (!DePriFile.Sections.OfType<ResourceMapSection>().Any())
        {
            ClosePriFile();
            MessageBox.Show("Incompatible PRI file.", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
        }
        else
        {
            ResourceRootPath = Path.GetDirectoryName(path);
            GetEntries();
            SetResourceRootPathCommand.RaiseCanExecuteChanged();
        }
    }


    private void ClosePriFile()
	{
		Entries.Clear();
		Candidates.Clear();
		PreviewContent = null;
		if (PriStream != null)
		{
			PriStream.Close();
			PriStream = null;
		}
        DePriFile = null;
		ResourceRootPath = null;
		SetResourceRootPathCommand.RaiseCanExecuteChanged();
	}

	private void GetEntries()
	{
		Entries.Clear();
		Candidates.Clear();
		ResourceMapSection sectionByRef = DePriFile.GetSectionByRef(DePriFile.PriDescriptorSection.PrimaryResourceMapSection.Value);
		HierarchicalSchemaSection sectionByRef2 = DePriFile.GetSectionByRef(sectionByRef.SchemaSection);
		Dictionary<ResourceMapEntry, EntryViewModel> dictionary = new Dictionary<ResourceMapEntry, EntryViewModel>();
		bool flag = false;
		do
		{
			foreach (ResourceMapScope scope in sectionByRef2.Scopes)
			{
				if (scope.FullName == string.Empty)
				{
					continue;
				}
				IList<EntryViewModel> list;
				if (scope.Parent == null)
				{
					list = Entries;
				}
				else if (scope.Parent.FullName == string.Empty)
				{
					list = Entries;
				}
				else
				{
					if (!dictionary.TryGetValue(scope.Parent, out var value))
					{
						flag = true;
						continue;
					}
					list = value.Children;
				}
				EntryViewModel entryViewModel = new EntryViewModel(scope);
				GetEntryType(entryViewModel);
				dictionary.Add(scope, entryViewModel);
				list.Add(entryViewModel);
			}
		}
		while (flag);
		foreach (ResourceMapItem item in sectionByRef2.Items)
		{
			if (dictionary.TryGetValue(item.Parent, out var value2))
			{
				value2.Children.Add(new EntryViewModel(item));
				GetEntryType(value2.Children.Last());
			}
		}
		CollapseStringResources();
	}

	private IEnumerable<Candidate> EnumerateCandidates(ResourceMapItem resourceMapItem)
	{
		ResourceMapSection primaryResourceMapSection = DePriFile.GetSectionByRef(DePriFile.PriDescriptorSection.PrimaryResourceMapSection.Value);
		if (!primaryResourceMapSection.CandidateSets.TryGetValue(resourceMapItem.Index, out var candidateSet))
		{
			yield break;
		}
		foreach (Candidate candidate in candidateSet.Candidates)
		{
			if (candidate != null)
			{
				yield return candidate;
			}
		}
	}

	private void GetCandidates(ResourceMapItem resourceMapItem)
	{
		Candidates.Clear();
		foreach (Candidate item2 in EnumerateCandidates(resourceMapItem))
		{
			CandidateViewModel item = new CandidateViewModel(DePriFile, PriStream, ResourceRootPath, resourceMapItem, item2);
			Candidates.Add(item);
		}
	}

	private void GetEntryType(EntryViewModel entry)
	{
		if (entry.ResourceMapEntry is ResourceMapScope)
		{
			entry.Icon = "/Assets/folder-horizontal.png";
			return;
		}
		entry.Icon = "/Assets/blue-document.png";
		ResourceMapItem resourceMapItem = (ResourceMapItem)entry.ResourceMapEntry;
		CandidateViewModel[] array = (from candidate in EnumerateCandidates(resourceMapItem)
			select new CandidateViewModel(DePriFile, PriStream, ResourceRootPath, resourceMapItem, candidate)).ToArray();
		if (array.Length == 0)
		{
			entry.Icon = "/Assets/document.png";
		}
		else if (array.All((CandidateViewModel c) => c.SourceNotFound || c.LocationNotFound))
		{
			entry.Icon = "/Assets/blue-document-attribute-x.png";
		}
		else if (array.All((CandidateViewModel c) => c.Candidate.Type == ResourceValueType.String || c.Candidate.Type == ResourceValueType.AsciiString || c.Candidate.Type == ResourceValueType.Utf8String))
		{
			entry.Icon = "/Assets/blue-document-attribute-s.png";
			entry.IsString = true;
		}
		else if (resourceMapItem.Name.EndsWith(".xbf", StringComparison.OrdinalIgnoreCase) || resourceMapItem.Name.EndsWith(".xaml", StringComparison.OrdinalIgnoreCase))
		{
			entry.Icon = "/Assets/blue-document-xaml.png";
		}
	}

	private void CollapseStringResources()
	{
		foreach (EntryViewModel entry in Entries)
		{
			if (entry.Type == EntryType.Scope)
			{
				CollapseStringResources(entry);
			}
		}
	}

	private void CollapseStringResources(EntryViewModel entry)
	{
		if (ContainsOnlyStringResources(entry))
		{
			Dictionary<EntryViewModel, string> dictionary = new Dictionary<EntryViewModel, string>();
			CollectStringResources(entry, string.Empty, dictionary);
			entry.Children.Clear();
			{
				foreach (KeyValuePair<EntryViewModel, string> item in dictionary)
				{
					entry.Children.Add(new StringEntryViewModel(item.Key.ResourceMapEntry, item.Value));
				}
				return;
			}
		}
		foreach (EntryViewModel child in entry.Children)
		{
			if (child.Type == EntryType.Scope)
			{
				CollapseStringResources(child);
			}
		}
	}

	private bool ContainsOnlyStringResources(EntryViewModel entry)
	{
		foreach (EntryViewModel child in entry.Children)
		{
			if (child.Type == EntryType.Scope)
			{
				if (!ContainsOnlyStringResources(child))
				{
					return false;
				}
			}
			else if (!child.IsString)
			{
				return false;
			}
		}
		return true;
	}

	private void CollectStringResources(EntryViewModel entry, string prefix, Dictionary<EntryViewModel, string> strings)
	{
		foreach (EntryViewModel child in entry.Children)
		{
			if (child.Type == EntryType.Scope)
			{
				CollectStringResources(child, ((prefix != "") ? (prefix + ".") : "") + child.ResourceMapEntry.Name, strings);
			}
			else
			{
				strings.Add(child, ((prefix != "") ? (prefix + ".") : "") + child.ResourceMapEntry.Name);
			}
		}
	}

	public void SelectedEntryChanged()
	{
		if (selectedEntry == null)
		{
			PreviewContent = null;
		}
		else if (selectedEntry.ResourceMapEntry is ResourceMapItem)
		{
			ResourceMapItem resourceMapItem = (ResourceMapItem)selectedEntry.ResourceMapEntry;
			GetCandidates(resourceMapItem);
			if (Candidates.Count > 0)
			{
				SelectedCandidate = Candidates.First();
			}
			else
			{
				PreviewContent = null;
			}
		}
	}

    private void OpenCommand_Execute()
    {
        Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
        openFileDialog.Filter = "Package Resource Index files (*.pri)|*.pri|XAML Binary Files (*.xbf)|*.xbf";
        if (openFileDialog.ShowDialog() == true)
        {
            if (openFileDialog.FileName.ToLower().EndsWith(".xbf"))
            {
                object obj = null;
                obj = new XbfPreviewPage(new XbfPreviewViewModel(File.ReadAllBytes(openFileDialog.FileName)));
                PreviewContent = obj;
            }
            else
            {
                OpenPriFile(openFileDialog.FileName);
            }
        }
    }
    private void SaveXbfCommand_Execute()
    {
        if (PreviewContent is XbfPreviewPage xbfPreviewPage)
        {
            // Log the actual type of PreviewContent
            Console.WriteLine($"PreviewContent type: {PreviewContent.GetType().FullName}");

            if (xbfPreviewPage.DataContext is XbfPreviewViewModel xbfPreviewViewModel)
            {
                // Use OpenFileDialog to allow the user to select an XBF file
                System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                openFileDialog.Filter = "XBF files (*.xbf)|*.xbf|All files (*.*)|*.*";
                openFileDialog.Title = "Select an XBF file";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the selected file path
                    string xbfFilePath = openFileDialog.FileName;

                    try
                    {
                        // Read the XBF file content
                        byte[] modifiedXbfData = File.ReadAllBytes(xbfFilePath);

                        // Replace the old XBF data in the PRI file
                        ReplaceXbfInPriFile(modifiedXbfData);

                        // Optionally, refresh the preview with the updated XBF data
                        // PreviewContent = new XbfPreviewPage(new XbfPreviewViewModel(modifiedXbfData));
                    }
                    catch (Exception ex)
                    {
                        // MessageBox.Show($"Error loading XBF file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Log the actual type of DataContext
                MessageBox.Show($"DataContext type: {xbfPreviewPage.DataContext?.GetType().FullName}");
            }
        }
        else
        {
            // Log the actual type of PreviewContent when it's not XbfPreviewPage
            MessageBox.Show($"Unexpected PreviewContent type: {PreviewContent?.GetType().FullName}");
        }
    }
    private void UpdateXbfInPriFile(ResourceMapItem resourceMapItem, int candidateIndex, byte[] modifiedXbfData)
    {
        ResourceMapSection primaryResourceMapSection = DePriFile.GetSectionByRef(DePriFile.PriDescriptorSection.PrimaryResourceMapSection.Value);

        if (primaryResourceMapSection.CandidateSets.TryGetValue(resourceMapItem.Index, out var candidateSet))
        {
            if (candidateIndex >= 0 && candidateIndex < candidateSet.Candidates.Count)
            {
                Candidate candidate = candidateSet.Candidates[candidateIndex];

                switch (candidate.Type)
                {
                    case ResourceValueType.EmbeddedData:
                        // Update the XBF data in the candidate
                        if (candidate.SourceFile != null)
                        {
                            // Handle the case when the XBF is sourced from a file
                            // You may need to implement this part based on your specific logic
                            // For example, update the file content with modifiedXbfData
                            // Example: File.WriteAllBytes(candidate.SourceFile.Path, modifiedXbfData);
                        }
                        else if (candidate.DataItem != null)
                        {
                            // Handle the case when the XBF is embedded as a data item
                            // Update the data item content with modifiedXbfData
                            ByteSpan dataItemSpan = DePriFile.GetDataItemByRef(candidate.DataItem.Value);
                            UpdateByteSpanData(dataItemSpan, modifiedXbfData);
                        }
                        else if (candidate.Data != null)
                        {
                            // Handle the case when the XBF is directly specified as data
                            // Update the data content with modifiedXbfData
                            ByteSpan dataSpan = candidate.Data.Value;
                            UpdateByteSpanData(dataSpan, modifiedXbfData);
                        }

                        // Save the modified PRI file
                        // SaveModifiedPriFile();
                        break;

                    default:
                        MessageBox.Show("Unsupported candidate type for updating XBF data.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                }
            }
        }
    }

    private void UpdateByteSpanData(ByteSpan byteSpan, byte[] newData)
    {
        // Ensure that the new data can fit into the existing byte span
        if (newData.Length <= byteSpan.Length)
        {
            // Move to the appropriate position in the FileStream
            PriStream.Seek(byteSpan.Offset, SeekOrigin.Begin);

            // Write the new data into the FileStream
            PriStream.Write(newData, 0, newData.Length);
        }
        else
        {
            MessageBox.Show("Error: New data size exceeds the existing ByteSpan size.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    static void SaveFileStream(FileStream fileStream, string outputPath)
    {
        // Create a new FileStream to the desired output path
        using (FileStream outputFileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Copy the content from the existing FileStream to the new FileStream
            fileStream.Seek(0, SeekOrigin.Begin);
            fileStream.CopyTo(outputFileStream);
        }
    }

    private void SaveModifiedPriFile()
    {
        // Create an instance of SaveFileDialog
        SaveFileDialog saveFileDialog = new SaveFileDialog();

        // Set the default file name and extension
        saveFileDialog.FileName = "depri.pri";
        saveFileDialog.DefaultExt = "pri";

        // Set the filter to display only text files
        saveFileDialog.Filter = "Package Resource Index file (*.pri)|*.pri|All files (*.*)|*.*";

        // Show the dialog and check if the user clicked OK
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
            string fileName = saveFileDialog.FileName;
            FileStream newFileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            DePriFile.Save(newFileStream);
            // newFileStream.Close(); // Close the stream manually if necessary
            Console.WriteLine("File saved successfully!");
        }
        else
        {
            Console.WriteLine("User canceled the operation.");
        }
    }

    private void ReplaceXbfInPriFile(byte[] modifiedXbfData)
    {
        if (SelectedEntry?.ResourceMapEntry is ResourceMapItem resourceMapItem && SelectedCandidate != null)
        {
            Candidate candidate = SelectedCandidate.Candidate;

            // Find the index of the candidate within the candidate set
            int candidateIndex = GetCandidateIndex(resourceMapItem, candidate);

            // Update the XBF data in the PRI file
            UpdateXbfInPriFile(resourceMapItem, candidateIndex, modifiedXbfData);

            // Optionally, update the preview with the modified XBF data
            PreviewContent = new XbfPreviewPage(new XbfPreviewViewModel(modifiedXbfData));
        }
    }
    private void HandleError(string message, Exception ex)
    {
        MessageBox.Show($"{message}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void SelectedCandidateChanged()
	{
		if (selectedCandidate == null)
		{
			return;
		}
		object data = selectedCandidate.GetData();
		if (data == null)
		{
			PreviewContent = null;
			return;
		}
		byte[] array = null;
		object obj = null;
		switch (selectedCandidate.Candidate.Type)
		{
		case ResourceValueType.Path:
		case ResourceValueType.AsciiPath:
		case ResourceValueType.Utf8Path:
		{
			string path = (selectedCandidate.Candidate.SourceFile.HasValue ? Path.GetDirectoryName(DePriFile.GetReferencedFileByRef(selectedCandidate.Candidate.SourceFile.Value).FullName) : ResourceRootPath);
			string path2 = Path.Combine(path, (string)data);
			if (File.Exists(path2))
			{
				array = File.ReadAllBytes(path2);
			}
			else
			{
				obj = new PathNotFoundPreviewPage(new PathNotFoundPreviewViewModel(path2), this);
			}
			break;
		}
		case ResourceValueType.String:
		case ResourceValueType.AsciiString:
		case ResourceValueType.Utf8String:
			obj = data;
			break;
		case ResourceValueType.EmbeddedData:
			array = (byte[])data;
			break;
		}
		if (obj == null)
		{
			string text = selectedEntry?.ResourceMapEntry.Name ?? "";
			obj = (text.EndsWith(".xbf", StringComparison.OrdinalIgnoreCase) ? new XbfPreviewPage(new XbfPreviewViewModel(array)) : ((!text.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase) && !text.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) && !text.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) && !text.EndsWith(".png", StringComparison.OrdinalIgnoreCase) && !text.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) && !text.EndsWith(".ico", StringComparison.OrdinalIgnoreCase) && !text.EndsWith(".tif", StringComparison.OrdinalIgnoreCase) && !text.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase)) ? (((array.Length < 3 || array[0] != 239 || array[1] != 187 || array[2] != 191) && (array.Length < 2 || array[0] != 239 || array[1] != byte.MaxValue) && (array.Length < 2 || array[0] != byte.MaxValue || array[1] != 239) && !array.All((byte b) => b >= 8 && b <= 127)) ? ((object)new BinaryPreviewPage(new BinaryPreviewViewModel(array))) : ((object)new TextPreviewPage(new TextPreviewViewModel(array)))) : new ImagePreviewPage(new ImagePreviewViewModel(array))));
		}
		PreviewContent = obj;
	}
}
