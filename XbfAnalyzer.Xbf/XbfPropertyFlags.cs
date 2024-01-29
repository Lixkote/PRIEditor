using System;

namespace XbfAnalyzer.Xbf;

[Flags]
public enum XbfPropertyFlags
{
	None = 0,
	IsXmlProperty = 1,
	IsMarkupDirective = 2,
	IsImplicitProperty = 4
}
