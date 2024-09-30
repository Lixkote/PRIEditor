using System.Windows;
using System.Windows.Markup;
using PRIExplorer.ViewModels;

namespace PRIExplorer;

public partial class MainWindow : Window, IComponentConnector
{
	private MainViewModel viewModel;

	public MainWindow()
	{
		InitializeComponent();
		viewModel = new MainViewModel();
		base.DataContext = viewModel;
	}

	private void resourceMapTreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
	{
		viewModel.SelectedEntry = (EntryViewModel)e.NewValue;
	}

	private void Window_PreviewDragOver(object sender, DragEventArgs e)
	{
		if (e.Data.GetData(DataFormats.FileDrop) is string[] array && array.Length == 1)
		{
			e.Effects = DragDropEffects.Copy;
			e.Handled = true;
		}
	}

	private void Window_Drop(object sender, DragEventArgs e)
	{
		if (e.Data.GetData(DataFormats.FileDrop) is string[] array && array.Length == 1)
		{
			viewModel.OpenPriFile(array[0]);
			e.Handled = true;
		}
	}
}
