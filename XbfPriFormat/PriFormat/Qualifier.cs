namespace XbfPriFormat;

public class Qualifier
{
	public ushort Index { get; }

	public QualifierType Type { get; }

	public ushort Priority { get; }

	public float FallbackScore { get; }

	public string Value { get; }

	internal Qualifier(ushort index, QualifierType type, ushort priority, float fallbackScore, string value)
	{
		Index = index;
		Type = type;
		Priority = priority;
		FallbackScore = fallbackScore;
		Value = value;
	}

	public override string ToString()
	{
		return $"Index: {Index} Type: {Type} Value: {Value} Priority: {Priority} FallbackScore: {FallbackScore}";
	}
}
