using System.IO;

namespace XbfAnalyzer.Xbf;

public class XbfType
{
	public XbfTypeFlags Flags { get; private set; }

	public XbfTypeNamespace Namespace { get; private set; }

	public string Name { get; private set; }

	public XbfType(XbfReader xbf, BinaryReader reader)
	{
		Flags = (XbfTypeFlags)reader.ReadInt32();
		int num = reader.ReadInt32();
		Namespace = xbf.TypeNamespaceTable[num];
		int num2 = reader.ReadInt32();
		Name = xbf.StringTable[num2];
	}
}
