using System.Collections.Generic;

namespace PriFormat;

public class ResourceMapScope : ResourceMapEntry
{
	public IReadOnlyList<ResourceMapEntry> Children { get; internal set; }

	internal ResourceMapScope(ushort index, ResourceMapScope parent, string name)
		: base(index, parent, name)
	{
	}

	public override string ToString()
	{
		return $"Scope {base.Index} {base.FullName} ({Children.Count} children)";
	}
}
