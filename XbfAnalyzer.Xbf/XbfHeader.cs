using System.IO;

namespace XbfAnalyzer.Xbf;

public class XbfHeader
{
	public byte[] MagicNumber { get; private set; }

	public uint MetadataSize { get; private set; }

	public uint NodeSize { get; private set; }

	public uint MajorFileVersion { get; private set; }

	public uint MinorFileVersion { get; private set; }

	public ulong StringTableOffset { get; private set; }

	public ulong AssemblyTableOffset { get; private set; }

	public ulong TypeNamespaceTableOffset { get; private set; }

	public ulong TypeTableOffset { get; private set; }

	public ulong PropertyTableOffset { get; private set; }

	public ulong XmlNamespaceTableOffset { get; private set; }

	public char[] Hash { get; private set; }

	public XbfHeader(BinaryReader reader)
	{
		byte[] array = reader.ReadBytes(4);
		if (array[0] != 88 || array[1] != 66 || array[2] != 70 || array[3] != 0)
		{
			// throw new InvalidDataException("File does not have XBF header");
		}
		MagicNumber = array;
		MetadataSize = reader.ReadUInt32();
		NodeSize = reader.ReadUInt32();
		MajorFileVersion = reader.ReadUInt32();
		MinorFileVersion = reader.ReadUInt32();
		StringTableOffset = reader.ReadUInt64();
		AssemblyTableOffset = reader.ReadUInt64();
		TypeNamespaceTableOffset = reader.ReadUInt64();
		TypeTableOffset = reader.ReadUInt64();
		PropertyTableOffset = reader.ReadUInt64();
		XmlNamespaceTableOffset = reader.ReadUInt64();
		Hash = reader.ReadChars(32);
	}
}
