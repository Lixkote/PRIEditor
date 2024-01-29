using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace PRIExplorer.Controls;

[ContentProperty("AdditionalContent")]
public partial class CheckerboardImage : UserControl, IComponentConnector
{
	public static readonly DependencyProperty AdditionalContentProperty = DependencyProperty.Register("AdditionalContent", typeof(object), typeof(CheckerboardImage), new PropertyMetadata(null));

	public object AdditionalContent
	{
		get
		{
			return GetValue(AdditionalContentProperty);
		}
		set
		{
			SetValue(AdditionalContentProperty, value);
		}
	}

	public CheckerboardImage()
	{
		InitializeComponent();
	}
}
