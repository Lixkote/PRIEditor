using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Markup;
using XbfPriFormat;

namespace PRIExplorer.Views;

public partial class ScopeDetailPage : Page, IComponentConnector
{
	private class StringResource
	{
		public string Key { get; }

		public string Value { get; }

		public string Qualifiers { get; }

		public StringResource(string key, string value, string qualifiers)
		{
			Key = key;
			Value = value;
			Qualifiers = qualifiers;
		}
	}

	private PriFile priFile;

	private Stream priStream;

	public ScopeDetailPage(PriFile priFile, Stream priStream, ResourceMapScope scope)
	{
		InitializeComponent();
		this.priFile = priFile;
		this.priStream = priStream;
		bool dotAsPathSeparator = scope.FullName.Contains("\\Resources");
		List<StringResource> list = new List<StringResource>();
		EnumerateStringResources(scope, string.Empty, dotAsPathSeparator, list);
		keyValueListView.ItemsSource = list;
	}

	private void EnumerateStringResources(ResourceMapScope scope, string pathPrefix, bool dotAsPathSeparator, List<StringResource> stringResources)
	{
		foreach (ResourceMapItem item in scope.Children.OfType<ResourceMapItem>())
		{
			string path = ((!dotAsPathSeparator) ? item.FullName : ((pathPrefix == string.Empty) ? item.Name : (pathPrefix + "." + item.Name)));
			AddStringResources(item, path, stringResources);
		}
		foreach (ResourceMapScope item2 in scope.Children.OfType<ResourceMapScope>())
		{
			string pathPrefix2 = ((pathPrefix == string.Empty) ? item2.Name : (pathPrefix + "." + item2.Name));
			EnumerateStringResources(item2, pathPrefix2, dotAsPathSeparator, stringResources);
		}
	}

	private void AddStringResources(ResourceMapItem item, string path, List<StringResource> stringResources)
	{
		ResourceMapSection sectionByRef = priFile.GetSectionByRef(priFile.PriDescriptorSection.PrimaryResourceMapSection.Value);
		DecisionInfoSection sectionByRef2 = priFile.GetSectionByRef(priFile.PriDescriptorSection.DecisionInfoSections.First());
		if (!sectionByRef.CandidateSets.TryGetValue(item.Index, out var value))
		{
			return;
		}
		foreach (Candidate candidate in value.Candidates)
		{
			if (candidate == null)
			{
				continue;
			}
			string value2;
			if (candidate.SourceFile.HasValue)
			{
				value2 = "external at " + priFile.GetReferencedFileByRef(candidate.SourceFile.Value).FullName;
			}
			else
			{
				ByteSpan byteSpan = ((!candidate.DataItem.HasValue) ? candidate.Data.Value : priFile.GetDataItemByRef(candidate.DataItem.Value));
				priStream.Seek(byteSpan.Offset, SeekOrigin.Begin);
				byte[] data;
				using (BinaryReader binaryReader = new BinaryReader(priStream, Encoding.Default, leaveOpen: true))
				{
					data = binaryReader.ReadBytes((int)byteSpan.Length);
				}
				value2 = GetCandidateDataAsString(candidate, data);
			}
			IReadOnlyList<Qualifier> qualifiers = sectionByRef2.QualifierSets[candidate.QualifierSet].Qualifiers;
			string qualifiers2 = string.Join(", ", qualifiers.Select((Qualifier q) => q.Type.ToString() + "=" + q.Value));
			StringResource item2 = new StringResource(path, value2, qualifiers2);
			stringResources.Add(item2);
		}
	}

	private string GetCandidateDataAsString(Candidate candidate, byte[] data)
	{
		string text = null;
		switch (candidate.Type)
		{
		case ResourceValueType.AsciiString:
			text = Encoding.ASCII.GetString(data);
			break;
		case ResourceValueType.Utf8String:
			text = Encoding.UTF8.GetString(data);
			break;
		case ResourceValueType.String:
			text = Encoding.Unicode.GetString(data);
			break;
		case ResourceValueType.Path:
		case ResourceValueType.AsciiPath:
		case ResourceValueType.Utf8Path:
			return "(external)";
		case ResourceValueType.EmbeddedData:
			return "(embedded)";
		}
		return text.TrimEnd(default(char));
	}
}
