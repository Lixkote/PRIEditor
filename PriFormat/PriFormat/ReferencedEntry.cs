namespace PriFormat;

public class ReferencedEntry
{
	private string fullName;

	public ReferencedFolder Parent { get; internal set; }

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

	internal ReferencedEntry(ReferencedFolder parent, string name)
	{
		Parent = parent;
		Name = name;
	}
}
