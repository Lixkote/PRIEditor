using System.Collections.Generic;

namespace XbfPriFormat;

public class QualifierSet
{
	public ushort Index { get; }

	public IReadOnlyList<Qualifier> Qualifiers { get; }

	internal QualifierSet(ushort index, IReadOnlyList<Qualifier> qualifiers)
	{
		Index = index;
		Qualifiers = qualifiers;
	}

	public override string ToString()
	{
		return $"Index: {Index} Qualifiers: {Qualifiers.Count}";
	}
}
