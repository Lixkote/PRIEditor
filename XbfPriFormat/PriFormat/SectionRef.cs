namespace XbfPriFormat;

public struct SectionRef<T> where T : Section
{
	internal int sectionIndex;
    internal ushort Index;

    internal SectionRef(int sectionIndex)
	{
		this.sectionIndex = sectionIndex;
	}

	public override string ToString()
	{
		return $"Section {typeof(T).Name} at index {sectionIndex}";
	}
}
