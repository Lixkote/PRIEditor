using System.Windows.Controls;
using System.Windows.Markup;
using PRIExplorer.ViewModels;

namespace PRIExplorer.Views;

public partial class PathNotFoundPreviewPage : Page, IComponentConnector
{
	public PathNotFoundPreviewPage(PathNotFoundPreviewViewModel viewModel, MainViewModel mainViewModel)
	{
		InitializeComponent();
		base.DataContext = viewModel;
		setResourceRootPathButton.DataContext = mainViewModel;
	}
}
