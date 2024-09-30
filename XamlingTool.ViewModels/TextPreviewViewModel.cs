using System.Text;

namespace PRIExplorer.ViewModels;

public class TextPreviewViewModel
{
	public string Text { get; }

	public TextPreviewViewModel(byte[] data)
	{
		Text = ((data.Length >= 3 && data[0] == 239 && data[1] == 187 && data[2] == 191) ? Encoding.UTF8 : (((data.Length < 2 || data[0] != 239 || data[1] != byte.MaxValue) && (data.Length < 2 || data[0] != byte.MaxValue || data[1] != 239)) ? Encoding.ASCII : Encoding.Unicode)).GetString(data);
	}
}
