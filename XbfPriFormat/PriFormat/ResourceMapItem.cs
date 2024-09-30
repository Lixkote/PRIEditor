namespace XbfPriFormat;

public class ResourceMapItem : ResourceMapEntry
{
	internal ResourceMapItem(ushort index, ResourceMapScope parent, string name)
		: base(index, parent, name)
	{
	}

	public override string ToString()
	{
		return $"Item {base.Index} {base.FullName}";
	}
}
