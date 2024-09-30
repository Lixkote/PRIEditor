namespace XbfPriFormat;

public class HierarchicalSchemaVersionInfo
{
	public ushort MajorVersion { get; }

	public ushort MinorVersion { get; }

	public uint Checksum { get; }

	public uint NumScopes { get; }

	public uint NumItems { get; }

	internal HierarchicalSchemaVersionInfo(ushort majorVersion, ushort minorVersion, uint checksum, uint numScopes, uint numItems)
	{
		MajorVersion = majorVersion;
		MinorVersion = minorVersion;
		Checksum = checksum;
		NumScopes = numScopes;
		NumItems = numItems;
	}
}
