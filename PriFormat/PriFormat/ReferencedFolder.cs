using System.Collections.Generic;

namespace PriFormat;

public class ReferencedFolder : ReferencedEntry
{
	public IReadOnlyList<ReferencedEntry> Children { get; internal set; }

	internal ReferencedFolder(ReferencedFolder parent, string name)
		: base(parent, name)
	{
	}
}
