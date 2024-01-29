namespace PriFormat;

public class Candidate
{
	public ushort QualifierSet { get; }

	public ResourceValueType Type { get; }

	public ReferencedFileRef? SourceFile { get; }

	public DataItemRef? DataItem { get; }

	public ByteSpan? Data { get; }

	internal Candidate(ushort qualifierSet, ResourceValueType type, ReferencedFileRef? sourceFile, DataItemRef dataItem)
	{
		QualifierSet = qualifierSet;
		Type = type;
		SourceFile = sourceFile;
		DataItem = dataItem;
		Data = null;
	}

	internal Candidate(ushort qualifierSet, ResourceValueType type, ByteSpan data)
	{
		QualifierSet = qualifierSet;
		Type = type;
		SourceFile = null;
		DataItem = null;
		Data = data;
	}
}
