using System.IO;

namespace XbfAnalyzer.Xbf;

public class XbfProperty
{
	public XbfPropertyFlags Flags { get; private set; }

	public XbfType Type { get; private set; }

	public string Name { get; private set; }

	public XbfProperty(XbfReader xbf, BinaryReader reader)
	{
		Flags = (XbfPropertyFlags)reader.ReadInt32();
		int num = reader.ReadInt32();
		Type = xbf.TypeTable[num];
		int num2 = reader.ReadInt32();
		Name = xbf.StringTable[num2];
	}
}
