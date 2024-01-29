using System.IO;

namespace XbfAnalyzer.Xbf;

public class XbfTypeNamespace
{
	public XbfAssembly Assembly { get; private set; }

	public string Name { get; set; }

	public XbfTypeNamespace(XbfReader xbf, BinaryReader reader)
	{
		int num = reader.ReadInt32();
		Assembly = xbf.AssemblyTable[num];
		int num2 = reader.ReadInt32();
		Name = xbf.StringTable[num2];
	}

	public XbfTypeNamespace()
	{
	}
}
