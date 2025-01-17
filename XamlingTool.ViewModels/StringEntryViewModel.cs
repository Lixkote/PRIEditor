using XbfPriFormat;

namespace PRIExplorer.ViewModels;

public class StringEntryViewModel : EntryViewModel
{
	public new string Name { get; }

	public StringEntryViewModel(ResourceMapEntry resourceMapEntry, string name)
		: base(resourceMapEntry)
	{
		Name = name;
		base.Icon = "/assets/blue-document-attribute-s.png";
	}
}
