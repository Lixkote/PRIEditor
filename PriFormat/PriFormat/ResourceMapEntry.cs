namespace PriFormat;

public class ResourceMapEntry
{
	private string fullName;

	public ushort Index { get; }

	public ResourceMapScope Parent { get; internal set; }

	public string Name { get; }

	public string FullName
	{
		get
		{
			if (fullName == null)
			{
				if (Parent == null)
				{
					fullName = Name;
				}
				else
				{
					fullName = Parent.FullName + "\\" + Name;
				}
			}
			return fullName;
		}
	}

	internal ResourceMapEntry(ushort index, ResourceMapScope parent, string name)
	{
		Index = index;
		Parent = parent;
		Name = name;
	}
}
