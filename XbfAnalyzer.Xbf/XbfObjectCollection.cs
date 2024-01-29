using System.Collections.Generic;
using System.Text;

namespace XbfAnalyzer.Xbf;

public class XbfObjectCollection : List<XbfObject>
{
	public override string ToString()
	{
		return ToString(0);
	}

	public string ToString(int indentLevel)
	{
		StringBuilder stringBuilder = new StringBuilder();
		using (Enumerator enumerator = GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				XbfObject current = enumerator.Current;
				stringBuilder.AppendLine(current.ToString(indentLevel));
			}
		}
		return stringBuilder.ToString();
	}
}
