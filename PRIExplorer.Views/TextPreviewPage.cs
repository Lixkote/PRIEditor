using System.Windows.Controls;
using System.Windows.Markup;
using PRIExplorer.ViewModels;

namespace PRIExplorer.Views;

public partial class TextPreviewPage : Page, IComponentConnector
{
	public TextPreviewPage(TextPreviewViewModel viewModel)
	{
		InitializeComponent();
		base.DataContext = viewModel;
	}
}
