using System.Collections.ObjectModel;
using XbfPriFormat;

namespace PRIExplorer.ViewModels;

public class EntryViewModel
{
	public ResourceMapEntry ResourceMapEntry { get; }

	public EntryType Type { get; }

	public string Icon { get; set; }

	public bool IsString { get; set; }

	public ObservableCollection<EntryViewModel> Children { get; }

	public string Name => ResourceMapEntry.Name;

	public EntryViewModel(ResourceMapEntry resourceMapEntry)
	{
		ResourceMapEntry = resourceMapEntry;
		Type = ((!(resourceMapEntry is ResourceMapScope)) ? EntryType.Item : EntryType.Scope);
		Children = new ObservableCollection<EntryViewModel>();
	}
}
