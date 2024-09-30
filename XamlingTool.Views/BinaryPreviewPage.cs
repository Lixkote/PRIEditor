using System.Windows.Controls;
using System.Windows.Markup;
using PRIExplorer.ViewModels;

namespace PRIExplorer.Views;

public partial class BinaryPreviewPage : Page, IComponentConnector
{
	public BinaryPreviewPage(BinaryPreviewViewModel viewModel)
	{
		InitializeComponent();
		base.DataContext = viewModel;
	}
}
