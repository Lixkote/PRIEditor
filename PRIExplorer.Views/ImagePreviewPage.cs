using System.Windows.Controls;
using System.Windows.Markup;
using PRIExplorer.ViewModels;

namespace PRIExplorer.Views;

public partial class ImagePreviewPage : Page, IComponentConnector
{
	public ImagePreviewPage(ImagePreviewViewModel viewModel)
	{
		InitializeComponent();
		base.DataContext = viewModel;
	}
}
