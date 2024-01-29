using System.IO;

namespace XbfAnalyzer.Xbf;

public class XbfAssembly
{
	public XbfAssemblyKind Kind { get; private set; }

	public string Name { get; private set; }

	public XbfAssembly(XbfReader xbf, BinaryReader reader)
	{
		Kind = (XbfAssemblyKind)reader.ReadInt32();
		int num = reader.ReadInt32();
		Name = xbf.StringTable[num];
	}
}
