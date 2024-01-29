using System.Collections.Generic;

namespace PriFormat;

public class Decision
{
	public ushort Index { get; }

	public IReadOnlyList<QualifierSet> QualifierSets { get; }

	internal Decision(ushort index, IReadOnlyList<QualifierSet> qualifierSets)
	{
		Index = index;
		QualifierSets = qualifierSets;
	}

	public override string ToString()
	{
		return $"Index: {Index} Qualifier sets: {QualifierSets.Count}";
	}
}
