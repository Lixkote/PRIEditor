namespace PriFormat.Pers;

public class GifPart
{
	public byte[] Data;

	public string Name;

	public int Frames;

	public int Offset;

	public int Size;

	public static bool IsGif(byte[] bytes)
	{
		if (bytes == null || bytes.Length < 5)
		{
			return false;
		}
		return bytes[0] == 71 && bytes[1] == 73 && bytes[2] == 70 && bytes[3] == 56 && bytes[4] == 57;
	}
}
