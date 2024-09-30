using System.Windows.Controls;
using System.Windows.Markup;
using PRIExplorer.ViewModels;

namespace PRIExplorer.Views;

public partial class XbfPreviewPage : Page, IComponentConnector
{
	public XbfPreviewPage(XbfPreviewViewModel viewModel)
	{
		InitializeComponent();
		base.DataContext = viewModel;
	}

}
