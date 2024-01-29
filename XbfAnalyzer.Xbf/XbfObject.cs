using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XbfAnalyzer.Xbf;

public class XbfObject
{
	private const string _indent = "    ";

	public string TypeName { get; set; }

	public string Name { get; set; }

	public string Uid { get; set; }

	public string Key { get; set; }

	public int ConnectionID { get; set; }

	public List<XbfObjectProperty> Properties { get; } = new List<XbfObjectProperty>();


	public XbfObjectCollection Children { get; } = new XbfObjectCollection();


	public override string ToString()
	{
		return ToString(0);
	}

	public virtual string ToString(int indentLevel)
	{
		string text = string.Concat(Enumerable.Repeat("    ", indentLevel));
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendFormat(text + "<{0}", TypeName);
		if (Name != null)
		{
			stringBuilder.AppendFormat(" x:Name=\"{0}\"", Name);
		}
		if (Uid != null)
		{
			stringBuilder.AppendFormat(" x:Uid=\"{0}\"", Uid);
		}
		if (Key != null)
		{
			stringBuilder.AppendFormat(" x:Key=\"{0}\"", Key);
		}
		ILookup<bool, XbfObjectProperty> lookup = Properties.ToLookup((XbfObjectProperty p) => p.Value is XbfObject || p.Value is XbfObjectCollection);
		XbfObjectProperty[] array = lookup[true].ToArray();
		XbfObjectProperty[] array2 = lookup[false].ToArray();
		if (array2.Length <= 4)
		{
			XbfObjectProperty[] array3 = array2;
			foreach (XbfObjectProperty xbfObjectProperty in array3)
			{
				stringBuilder.AppendFormat(" {0}=\"{1}\"", xbfObjectProperty.Name, xbfObjectProperty.Value);
			}
		}
		else
		{
			XbfObjectProperty[] array4 = array2;
			foreach (XbfObjectProperty xbfObjectProperty2 in array4)
			{
				stringBuilder.AppendLine().AppendFormat(text + "    {0}=\"{1}\"", xbfObjectProperty2.Name, xbfObjectProperty2.Value);
			}
		}
		if (array.Length == 0 && Children.Count == 0)
		{
			stringBuilder.Append(" />");
			return stringBuilder.ToString();
		}
		stringBuilder.AppendLine(">");
		XbfObjectProperty[] array5 = array;
		foreach (XbfObjectProperty xbfObjectProperty3 in array5)
		{
			XbfObjectCollection xbfObjectCollection = xbfObjectProperty3.Value as XbfObjectCollection;
			if (xbfObjectCollection == null || xbfObjectCollection.Count != 0)
			{
				string arg;
				if (xbfObjectProperty3.Name.Contains(":"))
				{
					int num = xbfObjectProperty3.Name.IndexOf(":");
					string text2 = xbfObjectProperty3.Name.Substring(0, num);
					arg = text2 + ":" + TypeName + "." + xbfObjectProperty3.Name.Remove(0, num + 1);
				}
				else
				{
					arg = TypeName + "." + xbfObjectProperty3.Name;
				}
				stringBuilder.AppendFormat(text + "    <{0}>", arg);
				stringBuilder.AppendLine();
				if (xbfObjectProperty3.Value is XbfObject)
				{
					stringBuilder.AppendLine(((XbfObject)xbfObjectProperty3.Value).ToString(indentLevel + 2));
				}
				else
				{
					stringBuilder.Append(xbfObjectCollection.ToString(indentLevel + 2));
				}
				stringBuilder.AppendFormat(text + "    </{0}>", arg);
				stringBuilder.AppendLine();
			}
		}
		foreach (XbfObject child in Children)
		{
			stringBuilder.AppendLine(child.ToString(indentLevel + 1));
		}
		stringBuilder.AppendFormat(text + "</{0}>", TypeName);
		return stringBuilder.ToString();
	}
}
