namespace PriFormat;

public class ResourceMapEntry
{
	private string fullName;

	public ushort Index { get; set; }

	public ResourceMapScope Parent { get; set; }

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
        set
        {
            // You can add validation logic if needed
            fullName = value;
        }
    }

    internal ResourceMapEntry(ushort index, ResourceMapScope parent, string name)
	{
		Index = index;
		Parent = parent;
		Name = name;
	}
}
