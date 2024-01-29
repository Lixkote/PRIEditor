namespace XbfAnalyzer.Xbf;

public class XbfObjectProperty
{
	public string Name { get; private set; }

	public object Value { get; private set; }

	public XbfObjectProperty(string name, object value)
	{
		Name = name;
		Value = value;
	}
}
