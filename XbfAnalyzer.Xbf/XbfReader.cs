using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace XbfAnalyzer.Xbf;

public class XbfReader
{
	private int _firstNodeSectionPosition;

	private Dictionary<string, string> _namespacePrefixes = new Dictionary<string, string>();

	private Stack<XbfObject> _rootObjectStack = new Stack<XbfObject>();

	private Stack<XbfObject> _objectStack = new Stack<XbfObject>();

	private Stack<XbfObjectCollection> _objectCollectionStack = new Stack<XbfObjectCollection>();

	public XbfHeader Header { get; private set; }

	public string[] StringTable { get; private set; }

	public XbfAssembly[] AssemblyTable { get; private set; }

	public XbfTypeNamespace[] TypeNamespaceTable { get; private set; }

	public XbfType[] TypeTable { get; private set; }

	public XbfProperty[] PropertyTable { get; private set; }

	public string[] XmlNamespaceTable { get; private set; }

	public XbfNodeSection[] NodeSectionTable { get; private set; }

	public XbfObject RootObject { get; private set; }

	public XbfReader(string path)
	{
		using FileStream input = File.OpenRead(path);
		BinaryReaderEx reader = new BinaryReaderEx(input, Encoding.Unicode);
		try
		{
			Header = new XbfHeader(reader);
			ReadStringTable(reader);
			AssemblyTable = ReadTable(reader, (BinaryReader r) => new XbfAssembly(this, r));
			TypeNamespaceTable = ReadTable(reader, (BinaryReader r) => new XbfTypeNamespace(this, r));
			TypeTable = ReadTable(reader, (BinaryReader r) => new XbfType(this, r));
			PropertyTable = ReadTable(reader, (BinaryReader r) => new XbfProperty(this, r));
			XmlNamespaceTable = ReadTable(reader, (BinaryReader r) => StringTable[r.ReadInt32()]);
			if (Header.MajorFileVersion >= 2)
			{
				NodeSectionTable = ReadTable(reader, (BinaryReader r) => new XbfNodeSection(this, reader));
				_firstNodeSectionPosition = (int)reader.BaseStream.Position;
				RootObject = ReadRootNodeSection(reader);
			}
		}
		finally
		{
			if (reader != null)
			{
				((IDisposable)reader).Dispose();
			}
		}
	}

	public XbfReader(Stream stream)
	{
		BinaryReaderEx reader = new BinaryReaderEx(stream, Encoding.Unicode);
		try
		{
			Header = new XbfHeader(reader);
			ReadStringTable(reader);
			AssemblyTable = ReadTable(reader, (BinaryReader r) => new XbfAssembly(this, r));
			TypeNamespaceTable = ReadTable(reader, (BinaryReader r) => new XbfTypeNamespace(this, r));
			List<XbfTypeNamespace> list = new List<XbfTypeNamespace>();
			string[] stringTable = StringTable;
			foreach (string text in stringTable)
			{
				if (text.StartsWith("http") || text.StartsWith("using:"))
				{
					XbfTypeNamespace item = new XbfTypeNamespace
					{
						Name = text
					};
					list.Add(item);
				}
			}
			TypeNamespaceTable = list.ToArray();
			TypeTable = ReadTable(reader, (BinaryReader r) => new XbfType(this, r));
			PropertyTable = ReadTable(reader, (BinaryReader r) => new XbfProperty(this, r));
			XmlNamespaceTable = ReadTable(reader, (BinaryReader r) => StringTable[r.ReadInt32()]);
			if (Header.MajorFileVersion >= 2)
			{
				NodeSectionTable = ReadTable(reader, (BinaryReader r) => new XbfNodeSection(this, reader));
				_firstNodeSectionPosition = (int)reader.BaseStream.Position;
				RootObject = ReadRootNodeSection(reader);
			}
		}
		finally
		{
			if (reader != null)
			{
				((IDisposable)reader).Dispose();
			}
		}
	}

	private string ReadString(BinaryReader reader)
	{
		return new string(reader.ReadChars(reader.ReadInt32()));
	}

	private void ReadStringTable(BinaryReader reader)
	{
		int num = reader.ReadInt32();
		string[] array = new string[num];
		bool flag = Header.MajorFileVersion >= 2;
		for (int i = 0; i < num; i++)
		{
			array[i] = ReadString(reader);
			if (flag && reader.ReadUInt16() != 0)
			{
				throw new InvalidDataException("Unexpected value");
			}
		}
		StringTable = array;
	}

	private T[] ReadTable<T>(BinaryReader reader, Func<BinaryReader, T> objectGenerator)
	{
		int num = reader.ReadInt32();
		T[] array = new T[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = objectGenerator(reader);
		}
		return array;
	}

	private XbfObject ReadRootNodeSection(BinaryReaderEx reader)
	{
		if (Header.MajorFileVersion != 2)
		{
			throw new NotSupportedException("Only XBF v2 files are supported.");
		}
		int num = _firstNodeSectionPosition + NodeSectionTable[0].NodeOffset;
		int endPosition = _firstNodeSectionPosition + NodeSectionTable[0].PositionalOffset;
		reader.BaseStream.Seek(num, SeekOrigin.Begin);
		_rootObjectStack.Clear();
		_objectStack.Clear();
		_objectCollectionStack.Clear();
		ReadRoot(reader, endPosition);
		if (_objectStack.Count != 1)
		{
			throw new InvalidDataException("_objectStack corrupted");
		}
		if (_objectCollectionStack.Count != 0)
		{
			throw new InvalidDataException("_objectCollectionStack corrupted");
		}
		return _objectStack.Pop();
	}

	private void ReadRoot(BinaryReaderEx reader, int endPosition)
	{
		XbfObject xbfObject = new XbfObject();
		_rootObjectStack.Push(xbfObject);
		_objectStack.Push(xbfObject);
		_objectCollectionStack.Push(xbfObject.Children);
		while (reader.BaseStream.Position < endPosition)
		{
			byte b = reader.ReadByte();
			switch (b)
			{
			case 3:
			case 18:
			{
				string text = XmlNamespaceTable[reader.ReadUInt16()];
				string text2 = ReadString(reader);
				_namespacePrefixes[text] = text2;
				text2 = (string.IsNullOrEmpty(text2) ? "xmlns" : ("xmlns:" + text2));
				xbfObject.Properties.Add(new XbfObjectProperty(text2, text));
				continue;
			}
			case 11:
			{
				string value = ReadString(reader);
				xbfObject.Properties.Add(new XbfObjectProperty("x:Class", value));
				continue;
			}
			case 23:
				break;
			case 26:
			case 27:
			{
				string propertyName = GetPropertyName(reader.ReadUInt16());
				object propertyValue = GetPropertyValue(reader);
				_objectStack.Peek().Properties.Add(new XbfObjectProperty(propertyName, propertyValue));
				continue;
			}
			default:
				throw new InvalidDataException($"Unrecognized character 0x{b:X2} in node stream");
			}
			xbfObject.TypeName = GetTypeName(reader.ReadUInt16());
			ReadNodes(reader, endPosition);
			break;
		}
		if (_rootObjectStack.Pop() != xbfObject)
		{
			throw new InvalidDataException("_rootObjectStack corrupted");
		}
		if (_objectStack.Peek() != xbfObject)
		{
			throw new InvalidDataException("_objectStack corrupted");
		}
	}

	private void ReadNodes(BinaryReaderEx reader, int endPosition, bool readSingleObject = false, bool readSingleNode = false)
	{
		XbfObject xbfObject = null;
		while (reader.BaseStream.Position < endPosition)
		{
			byte b = reader.ReadByte();
			switch (b)
			{
			case 4:
				if (_objectCollectionStack.Peek() != _objectStack.Peek().Children)
				{
					object propertyValue6 = GetPropertyValue(reader);
					XbfObject xbfObject5 = new XbfObject
					{
						TypeName = "Verbatim"
					};
					xbfObject5.Properties.Add(new XbfObjectProperty("Value", propertyValue6));
					_objectStack.Push(xbfObject5);
				}
				else if (_objectStack.Peek() == _rootObjectStack.Peek())
				{
					object propertyValue7 = GetPropertyValue(reader);
					_objectStack.Peek().Properties.Add(new XbfObjectProperty("x:Class", propertyValue7));
				}
				else
				{
					object propertyValue8 = GetPropertyValue(reader);
				}
				break;
			case 12:
				_objectStack.Peek().ConnectionID = (int)GetPropertyValue(reader);
				break;
			case 13:
				_objectStack.Peek().Name = GetPropertyValue(reader).ToString();
				break;
			case 14:
				_objectStack.Peek().Uid = GetPropertyValue(reader).ToString();
				break;
			case 17:
				ReadDataTemplate(reader);
				break;
			case 26:
			case 27:
			{
				string propertyName6 = GetPropertyName(reader.ReadUInt16());
				object propertyValue5 = GetPropertyValue(reader);
				_objectStack.Peek().Properties.Add(new XbfObjectProperty(propertyName6, propertyValue5));
				break;
			}
			case 29:
			{
				string propertyName7 = GetPropertyName(reader.ReadUInt16());
				string typeName2 = GetTypeName(reader.ReadUInt16());
				_objectStack.Peek().Properties.Add(new XbfObjectProperty(propertyName7, typeName2));
				break;
			}
			case 30:
			{
				string propertyName4 = GetPropertyName(reader.ReadUInt16());
				object propertyValue4 = GetPropertyValue(reader);
				propertyValue4 = $"{{StaticResource {propertyValue4}}}";
				_objectStack.Peek().Properties.Add(new XbfObjectProperty(propertyName4, propertyValue4));
				break;
			}
			case 31:
			{
				string propertyName2 = GetPropertyName(reader.ReadUInt16());
				string propertyName3 = GetPropertyName(reader.ReadUInt16());
				propertyName3 = $"{{TemplateBinding {propertyName3}}}";
				_objectStack.Peek().Properties.Add(new XbfObjectProperty(propertyName2, propertyName3));
				break;
			}
			case 36:
			{
				string propertyName = GetPropertyName(reader.ReadUInt16());
				object propertyValue2 = GetPropertyValue(reader);
				propertyValue2 = $"{{ThemeResource {propertyValue2}}}";
				_objectStack.Peek().Properties.Add(new XbfObjectProperty(propertyName, propertyValue2));
				break;
			}
			case 34:
			{
				object propertyValue10 = GetPropertyValue(reader);
				XbfObject xbfObject7 = new XbfObject
				{
					TypeName = "StaticResource"
				};
				xbfObject7.Properties.Add(new XbfObjectProperty("ResourceKey", propertyValue10));
				_objectStack.Push(xbfObject7);
				if (readSingleObject)
				{
					return;
				}
				break;
			}
			case 35:
			{
				object propertyValue9 = GetPropertyValue(reader);
				XbfObject xbfObject6 = new XbfObject
				{
					TypeName = "ThemeResource"
				};
				xbfObject6.Properties.Add(new XbfObjectProperty("ResourceKey", propertyValue9));
				_objectStack.Push(xbfObject6);
				if (readSingleObject)
				{
					return;
				}
				break;
			}
			case 19:
			{
				string propertyName8 = GetPropertyName(reader.ReadUInt16());
				XbfObjectCollection xbfObjectCollection = new XbfObjectCollection();
				_objectStack.Peek().Properties.Add(new XbfObjectProperty(propertyName8, xbfObjectCollection));
				_objectCollectionStack.Push(xbfObjectCollection);
				break;
			}
			case 2:
				_objectCollectionStack.Pop();
				break;
			case 20:
			{
				XbfObject xbfObject4 = new XbfObject();
				xbfObject4.TypeName = GetTypeName(reader.ReadUInt16());
				_objectStack.Push(xbfObject4);
				_objectCollectionStack.Push(xbfObject4.Children);
				if (readSingleObject && xbfObject == null)
				{
					xbfObject = xbfObject4;
				}
				break;
			}
			case 33:
				if (_objectCollectionStack.Count > 0 && _objectCollectionStack.Peek() == _objectStack.Peek().Children)
				{
					_objectCollectionStack.Pop();
				}
				if ((readSingleObject && _objectStack.Peek() == xbfObject) || _objectStack.Peek() == _rootObjectStack.Peek())
				{
					return;
				}
				break;
			case 7:
			case 32:
			{
				string propertyName5 = GetPropertyName(reader.ReadUInt16());
				XbfObject value2 = _objectStack.Pop();
				_objectStack.Peek().Properties.Add(new XbfObjectProperty(propertyName5, value2));
				break;
			}
			case 8:
			case 9:
			{
				XbfObject item = _objectStack.Pop();
				_objectCollectionStack.Peek().Add(item);
				break;
			}
			case 10:
			{
				XbfObject xbfObject3 = _objectStack.Pop();
				xbfObject3.Key = GetPropertyValue(reader).ToString();
				_objectCollectionStack.Peek().Add(xbfObject3);
				break;
			}
			case 21:
			case 22:
			{
				XbfObject xbfObject2 = new XbfObject();
				xbfObject2.TypeName = GetTypeName(reader.ReadUInt16());
				object propertyValue3 = GetPropertyValue(reader);
				xbfObject2.Properties.Add(new XbfObjectProperty("Value", propertyValue3));
				_objectStack.Push(xbfObject2);
				if (readSingleObject && xbfObject == null)
				{
					xbfObject = xbfObject2;
				}
				break;
			}
			case 15:
				ReadNodeSectionReference(reader);
				break;
			case 18:
			case 23:
				reader.BaseStream.Seek(-1L, SeekOrigin.Current);
				ReadRoot(reader, endPosition);
				if (readSingleObject && xbfObject == null)
				{
					return;
				}
				break;
			case 11:
			{
				string value = ReadString(reader);
				_objectStack.Peek().Properties.Add(new XbfObjectProperty("x:Class", value));
				break;
			}
			case 24:
			case 25:
			{
				string typeName = GetTypeName(reader.ReadUInt16());
				object propertyValue = GetPropertyValue(reader);
				_objectStack.Peek().Properties.Add(new XbfObjectProperty("x:Class", typeName));
				_objectStack.Peek().Properties.Add(new XbfObjectProperty("x:Arguments", propertyValue));
				break;
			}
			case 139:
				_objectStack.Pop();
				break;
			case 38:
				ReadConditionalProperty(reader, endPosition);
				break;
			default:
				throw new InvalidDataException($"Unrecognized character 0x{b:X2} while parsing object");
			case 0:
			case 1:
				break;
			}
			if (readSingleNode)
			{
				break;
			}
		}
	}

	private void ReadConditionalProperty(BinaryReaderEx reader, int endPos)
	{
		string markupTypeName = GetMarkupTypeName(reader.ReadUInt16());
		string text = StringTable[reader.ReadUInt16()];
		byte b = reader.ReadByte();
		if (b == 27 || b == 26)
		{
			int num = reader.ReadUInt16();
			string text2;
			string text3;
			if (((uint)num & 0x8000u) != 0)
			{
				text2 = XbfFrameworkTypes.GetNameForPropertyID(num & -32769) ?? $"UnknownProperty0x{num:X4}";
				text3 = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
			}
			else
			{
				text2 = PropertyTable[num].Name;
				text3 = PropertyTable[num].Type.Namespace.Name;
			}
			object propertyValue = GetPropertyValue(reader);
			reader.ReadByte();
			string value;
			string xmlnsPrefix2;
			if (text3 == "http://schemas.microsoft.com/client/2007")
			{
				value = "http://schemas.microsoft.com/winfx/2006/xaml/presentation?" + markupTypeName + "(" + text + ")";
				string text4 = text.Replace(" ", string.Empty).Replace('"'.ToString(), string.Empty).Replace(",", string.Empty)
					.Replace("(", string.Empty)
					.Replace(")", string.Empty)
					.Replace(";", string.Empty)
					.Replace("?", string.Empty)
					.Replace(":", string.Empty)
					.Replace(".", string.Empty)
					.Replace("'", string.Empty)
					.Replace("/", string.Empty)
					.Replace("\\", string.Empty)
					.Replace("=", string.Empty)
					.Replace("+", string.Empty)
					.Replace("-", string.Empty)
					.Replace("@", string.Empty)
					.Replace("#", string.Empty)
					.Replace("$", string.Empty)
					.Replace("%", string.Empty)
					.Replace("^", string.Empty)
					.Replace("&", string.Empty)
					.Replace("*", string.Empty)
					.Replace("`", string.Empty)
					.Replace("~", string.Empty)
					.Replace("!", string.Empty)
					.Replace("{", string.Empty)
					.Replace("}", string.Empty)
					.Replace("[", string.Empty)
					.Replace("]", string.Empty)
					.Replace("<", string.Empty)
					.Replace(">", string.Empty);
				xmlnsPrefix2 = markupTypeName + text4;
			}
			else
			{
				value = text3 + "?" + markupTypeName + "(" + text + ")";
				string text5 = text.Replace(" ", string.Empty).Replace('"'.ToString(), string.Empty).Replace(",", string.Empty)
					.Replace("(", string.Empty)
					.Replace(")", string.Empty)
					.Replace(";", string.Empty)
					.Replace("?", string.Empty)
					.Replace(":", string.Empty)
					.Replace(".", string.Empty)
					.Replace("'", string.Empty)
					.Replace("/", string.Empty)
					.Replace("\\", string.Empty)
					.Replace("=", string.Empty)
					.Replace("+", string.Empty)
					.Replace("-", string.Empty)
					.Replace("@", string.Empty)
					.Replace("#", string.Empty)
					.Replace("$", string.Empty)
					.Replace("%", string.Empty)
					.Replace("^", string.Empty)
					.Replace("&", string.Empty)
					.Replace("*", string.Empty)
					.Replace("`", string.Empty)
					.Replace("~", string.Empty)
					.Replace("!", string.Empty)
					.Replace("{", string.Empty)
					.Replace("}", string.Empty)
					.Replace("[", string.Empty)
					.Replace("]", string.Empty)
					.Replace("<", string.Empty)
					.Replace(">", string.Empty);
				if (text3 != "http://schemas.microsoft.com/winfx/2006/xaml/presentation" && !string.IsNullOrWhiteSpace(_namespacePrefixes[text3]))
				{
					text5 += _namespacePrefixes[text3];
				}
				xmlnsPrefix2 = markupTypeName + text5;
			}
			_objectStack.Peek().Properties.Add(new XbfObjectProperty(xmlnsPrefix2 + ":" + text2, propertyValue));
			if (!_rootObjectStack.ElementAt(0).Properties.Any((XbfObjectProperty i) => i.Name == "xmlns:" + xmlnsPrefix2))
			{
				_rootObjectStack.ElementAt(0).Properties.Add(new XbfObjectProperty("xmlns:" + xmlnsPrefix2, value));
			}
			return;
		}
		reader.BaseStream.Seek(-1L, SeekOrigin.Current);
		bool flag = false;
		string empty = string.Empty;
		string empty2 = string.Empty;
		string value2 = string.Empty;
		string xmlnsPrefix = string.Empty;
		while (!flag)
		{
			switch (reader.ReadByte())
			{
			case 7:
			case 32:
			{
				int num2 = reader.ReadUInt16();
				byte b2 = reader.ReadByte();
				reader.BaseStream.Seek(-1L, SeekOrigin.Current);
				if (b2 == 39)
				{
					if (((uint)num2 & 0x8000u) != 0)
					{
						empty2 = XbfFrameworkTypes.GetNameForPropertyID(num2 & -32769) ?? $"UnknownProperty0x{num2:X4}";
						empty = "http://schemas.microsoft.com/winfx/2006/xaml/presentation";
					}
					else
					{
						empty2 = PropertyTable[num2].Name;
						empty = PropertyTable[num2].Type.Namespace.Name;
					}
					if (empty == "http://schemas.microsoft.com/client/2007")
					{
						value2 = "http://schemas.microsoft.com/winfx/2006/xaml/presentation?" + markupTypeName + "(" + text + ")";
						string text6 = text.Replace(" ", string.Empty).Replace('"'.ToString(), string.Empty).Replace(",", string.Empty)
							.Replace("(", string.Empty)
							.Replace(")", string.Empty)
							.Replace(";", string.Empty)
							.Replace("?", string.Empty)
							.Replace(":", string.Empty)
							.Replace(".", string.Empty)
							.Replace("'", string.Empty)
							.Replace("/", string.Empty)
							.Replace("\\", string.Empty)
							.Replace("=", string.Empty)
							.Replace("+", string.Empty)
							.Replace("-", string.Empty)
							.Replace("@", string.Empty)
							.Replace("#", string.Empty)
							.Replace("$", string.Empty)
							.Replace("%", string.Empty)
							.Replace("^", string.Empty)
							.Replace("&", string.Empty)
							.Replace("*", string.Empty)
							.Replace("`", string.Empty)
							.Replace("~", string.Empty)
							.Replace("!", string.Empty)
							.Replace("{", string.Empty)
							.Replace("}", string.Empty)
							.Replace("[", string.Empty)
							.Replace("]", string.Empty)
							.Replace("<", string.Empty)
							.Replace(">", string.Empty);
						xmlnsPrefix = markupTypeName + text6;
					}
					else
					{
						value2 = empty + "?" + markupTypeName + "(" + text + ")";
						string text7 = text.Replace(" ", string.Empty).Replace('"'.ToString(), string.Empty).Replace(",", string.Empty)
							.Replace("(", string.Empty)
							.Replace(")", string.Empty)
							.Replace(";", string.Empty)
							.Replace("?", string.Empty)
							.Replace(":", string.Empty)
							.Replace(".", string.Empty)
							.Replace("'", string.Empty)
							.Replace("/", string.Empty)
							.Replace("\\", string.Empty)
							.Replace("=", string.Empty)
							.Replace("+", string.Empty)
							.Replace("-", string.Empty)
							.Replace("@", string.Empty)
							.Replace("#", string.Empty)
							.Replace("$", string.Empty)
							.Replace("%", string.Empty)
							.Replace("^", string.Empty)
							.Replace("&", string.Empty)
							.Replace("*", string.Empty)
							.Replace("`", string.Empty)
							.Replace("~", string.Empty)
							.Replace("!", string.Empty)
							.Replace("{", string.Empty)
							.Replace("}", string.Empty)
							.Replace("[", string.Empty)
							.Replace("]", string.Empty)
							.Replace("<", string.Empty)
							.Replace(">", string.Empty);
						if (empty != "http://schemas.microsoft.com/winfx/2006/xaml/presentation" && !string.IsNullOrWhiteSpace(_namespacePrefixes[empty]))
						{
							text7 += _namespacePrefixes[empty];
						}
						xmlnsPrefix = markupTypeName + text7;
					}
					XbfObject value3 = _objectStack.Pop();
					_objectStack.Peek().Properties.Add(new XbfObjectProperty(xmlnsPrefix + ":" + empty2, value3));
				}
				else
				{
					reader.BaseStream.Seek(-3L, SeekOrigin.Current);
					ReadNodes(reader, endPos, readSingleObject: false, readSingleNode: true);
				}
				break;
			}
			case 39:
				flag = true;
				if (!_rootObjectStack.ElementAt(0).Properties.Any((XbfObjectProperty i) => i.Name == "xmlns:" + xmlnsPrefix))
				{
					_rootObjectStack.ElementAt(0).Properties.Add(new XbfObjectProperty("xmlns:" + xmlnsPrefix, value2));
				}
				break;
			case 38:
				ReadConditionalProperty(reader, endPos);
				break;
			default:
				reader.BaseStream.Seek(-1L, SeekOrigin.Current);
				ReadNodes(reader, endPos, readSingleObject: false, readSingleNode: true);
				break;
			}
		}
	}

	private void ReadNodeSectionReference(BinaryReaderEx reader)
	{
		XbfNodeSection nodeSection = NodeSectionTable[reader.Read7BitEncodedInt()];
		if (reader.ReadUInt16() != 0)
		{
			throw new InvalidDataException("Unexpected value");
		}
		int num = reader.Read7BitEncodedInt();
		switch (num)
		{
		case 2:
		case 8:
			ReadStyle(reader, nodeSection);
			break;
		case 7:
		case 10:
			ReadResourceDictionary(reader, nodeSection, extended: false);
			break;
		case 371:
			ReadResourceDictionary(reader, nodeSection, extended: true);
			break;
		case 5:
			SkipVisualStateBytes(reader);
			ReadNodeSection(reader, nodeSection);
			break;
		case 6:
		case 9:
			ReadDeferredElement(reader, nodeSection, extended: true);
			break;
		case 746:
			ReadDeferredElement(reader, nodeSection, extended: false);
			break;
		default:
			throw new InvalidDataException($"Unknown node type {num} while parsing referenced code section");
		}
	}

	private void ReadDataTemplate(BinaryReaderEx reader)
	{
		string propertyName = GetPropertyName(reader.ReadUInt16());
		XbfNodeSection nodeSection = NodeSectionTable[reader.Read7BitEncodedInt()];
		int num = reader.Read7BitEncodedInt();
		int num2 = reader.Read7BitEncodedInt();
		for (int i = 0; i < num; i++)
		{
			string text = StringTable[reader.ReadUInt16()];
		}
		for (int j = 0; j < num2; j++)
		{
			string text2 = StringTable[reader.ReadUInt16()];
		}
		ReadNodeSection(reader, nodeSection);
		XbfObject value = _objectStack.Pop();
		_objectStack.Peek().Properties.Add(new XbfObjectProperty(propertyName, value));
	}

	private void ReadStyle(BinaryReaderEx reader, XbfNodeSection nodeSection)
	{
		int num = reader.Read7BitEncodedInt();
		for (int i = 0; i < num; i++)
		{
			int num2 = reader.ReadByte();
			string text = null;
			string text2 = null;
			object obj = null;
			int offset = 0;
			switch (num2)
			{
			case 1:
			case 2:
			case 8:
				text = StringTable[reader.ReadUInt16()];
				text2 = GetTypeName(reader.ReadUInt16());
				offset = reader.Read7BitEncodedInt();
				break;
			case 17:
			case 18:
			case 24:
				text = GetPropertyName(reader.ReadUInt16());
				offset = reader.Read7BitEncodedInt();
				break;
			case 32:
				text = StringTable[reader.ReadUInt16()];
				text2 = GetTypeName(reader.ReadUInt16());
				obj = GetPropertyValue(reader);
				break;
			case 48:
				text = GetPropertyName(reader.ReadUInt16());
				obj = GetPropertyValue(reader);
				break;
			default:
				throw new InvalidDataException("Unexpected value");
			}
			if (num2 == 8 || num2 == 24)
			{
				obj = ReadObjectInNodeSection(reader, nodeSection, offset);
			}
			if (obj != null)
			{
				XbfObject xbfObject = new XbfObject();
				xbfObject.TypeName = "Setter";
				xbfObject.Properties.Add(new XbfObjectProperty("Property", text));
				xbfObject.Properties.Add(new XbfObjectProperty("Value", obj));
				_objectCollectionStack.Peek().Add(xbfObject);
			}
			else
			{
				XbfObject xbfObject2 = new XbfObject();
				xbfObject2.TypeName = "Setter";
				xbfObject2.Properties.Add(new XbfObjectProperty("Property", text));
				_objectCollectionStack.Peek().Add(xbfObject2);
				_objectStack.Push(xbfObject2);
				ReadNodeInNodeSection(reader, nodeSection, offset);
				_objectStack.Pop();
			}
		}
	}

	private void ReadDeferredElement(BinaryReaderEx reader, XbfNodeSection nodeSection, bool extended)
	{
		string text = StringTable[reader.ReadUInt16()];
		if (extended)
		{
			int num = reader.Read7BitEncodedInt();
			for (int i = 0; i < num; i++)
			{
				string propertyName = GetPropertyName(reader.ReadUInt16());
				object propertyValue = GetPropertyValue(reader);
			}
		}
		ReadNodeSection(reader, nodeSection);
		XbfObject item = _objectStack.Pop();
		XbfObject xbfObject = _objectStack.Peek();
		xbfObject.Children.Add(item);
	}

	private void ReadNodeSection(BinaryReaderEx reader, XbfNodeSection nodeSection)
	{
		long position = reader.BaseStream.Position;
		int num = _firstNodeSectionPosition + nodeSection.NodeOffset;
		int endPosition = _firstNodeSectionPosition + nodeSection.PositionalOffset;
		reader.BaseStream.Position = num;
		ReadNodes(reader, endPosition);
		reader.BaseStream.Position = position;
	}

	private XbfObject ReadObjectInNodeSection(BinaryReaderEx reader, XbfNodeSection nodeSection, int offset)
	{
		long position = reader.BaseStream.Position;
		int num = _firstNodeSectionPosition + nodeSection.NodeOffset;
		int num2 = _firstNodeSectionPosition + nodeSection.PositionalOffset;
		reader.BaseStream.Position = num + offset;
		int count = _objectStack.Count;
		int count2 = _objectCollectionStack.Count;
		ReadNodes(reader, int.MaxValue, readSingleObject: true);
		XbfObject result = _objectStack.Pop();
		if (_objectStack.Count != count)
		{
			throw new InvalidDataException("_objectStack corrupted");
		}
		if (_objectCollectionStack.Count != count2)
		{
			throw new InvalidDataException("_objectCollectionStack corrupted");
		}
		reader.BaseStream.Position = position;
		return result;
	}

	private void ReadNodeInNodeSection(BinaryReaderEx reader, XbfNodeSection nodeSection, int offset)
	{
		long position = reader.BaseStream.Position;
		int num = _firstNodeSectionPosition + nodeSection.NodeOffset;
		int num2 = _firstNodeSectionPosition + nodeSection.PositionalOffset;
		reader.BaseStream.Position = num + offset;
		int count = _objectStack.Count;
		int count2 = _objectCollectionStack.Count;
		ReadNodes(reader, int.MaxValue, readSingleObject: false, readSingleNode: true);
		if (_objectStack.Count != count)
		{
			throw new InvalidDataException("_objectStack corrupted");
		}
		if (_objectCollectionStack.Count != count2)
		{
			throw new InvalidDataException("_objectCollectionStack corrupted");
		}
		reader.BaseStream.Position = position;
	}

	private void ReadResourceDictionary(BinaryReaderEx reader, XbfNodeSection nodeSection, bool extended)
	{
		int num = reader.Read7BitEncodedInt();
		for (int i = 0; i < num; i++)
		{
			string key = StringTable[reader.ReadUInt16()];
			int offset = reader.Read7BitEncodedInt();
			XbfObject xbfObject = ReadObjectInNodeSection(reader, nodeSection, offset);
			xbfObject.Key = key;
			_objectCollectionStack.Peek().Add(xbfObject);
		}
		int num2 = reader.Read7BitEncodedInt();
		for (int j = 0; j < num2; j++)
		{
			string text = StringTable[reader.ReadUInt16()];
		}
		int num3 = reader.Read7BitEncodedInt();
		for (int k = 0; k < num3; k++)
		{
			string text2 = StringTable[reader.ReadUInt16()];
			int offset2 = reader.Read7BitEncodedInt();
			XbfObject item = ReadObjectInNodeSection(reader, nodeSection, offset2);
			_objectCollectionStack.Peek().Add(item);
		}
		if (extended && reader.Read7BitEncodedInt() != 0)
		{
			throw new InvalidDataException("Unexpected value");
		}
		int num4 = reader.Read7BitEncodedInt();
		for (int l = 0; l < num4; l++)
		{
			string text3 = StringTable[reader.ReadUInt16()];
		}
	}

	private void SkipVisualStateBytes(BinaryReaderEx reader)
	{
		int num = reader.Read7BitEncodedInt();
		int[] array = new int[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = reader.Read7BitEncodedInt();
		}
		int num2 = reader.Read7BitEncodedInt();
		if (num != num2)
		{
			throw new InvalidDataException("Visual state counts did not match");
		}
		XbfObject[] array2 = new XbfObject[num2];
		for (int j = 0; j < num2; j++)
		{
			int num3 = reader.ReadUInt16();
			reader.Read7BitEncodedInt();
			reader.Read7BitEncodedInt();
			int num4 = reader.Read7BitEncodedInt();
			for (int k = 0; k < num4; k++)
			{
				int num5 = reader.Read7BitEncodedInt();
			}
			int num6 = reader.Read7BitEncodedInt();
			for (int l = 0; l < num6; l++)
			{
				int num7 = reader.Read7BitEncodedInt();
				for (int m = 0; m < num7; m++)
				{
					reader.Read7BitEncodedInt();
				}
			}
			int num8 = reader.Read7BitEncodedInt();
			for (int n = 0; n < num8; n++)
			{
				int num9 = reader.Read7BitEncodedInt();
			}
			int num10 = reader.Read7BitEncodedInt();
			for (int num11 = 0; num11 < num10; num11++)
			{
				int num12 = reader.Read7BitEncodedInt();
			}
			if (reader.Read7BitEncodedInt() != 0)
			{
				throw new InvalidDataException("Unexpected value");
			}
			XbfObject xbfObject = new XbfObject();
			xbfObject.TypeName = "VisualState";
			xbfObject.Name = StringTable[num3];
			array2[j] = xbfObject;
		}
		int num13 = reader.Read7BitEncodedInt();
		XbfObject[] array3 = new XbfObject[num13];
		for (int num14 = 0; num14 < num13; num14++)
		{
			int num15 = reader.ReadUInt16();
			reader.Read7BitEncodedInt();
			int num16 = reader.Read7BitEncodedInt();
			XbfObject xbfObject2 = new XbfObject();
			xbfObject2.TypeName = "VisualStateGroup";
			xbfObject2.Name = StringTable[num15];
			List<XbfObject> list = new List<XbfObject>();
			for (int num17 = 0; num17 < array.Length; num17++)
			{
				if (array[num17] == num14)
				{
					list.Add(array2[num17]);
				}
			}
			if (list.Count > 0)
			{
				xbfObject2.Properties.Add(new XbfObjectProperty("States", list));
			}
			array3[num14] = xbfObject2;
		}
		int num18 = reader.Read7BitEncodedInt();
		for (int num19 = 0; num19 < num18; num19++)
		{
			string text = StringTable[reader.ReadUInt16()];
			string text2 = StringTable[reader.ReadUInt16()];
			int num20 = reader.Read7BitEncodedInt();
		}
		reader.Read7BitEncodedInt();
		int num21 = reader.Read7BitEncodedInt();
		for (int num22 = 0; num22 < num21; num22++)
		{
			int num23 = reader.Read7BitEncodedInt();
			int num24 = reader.Read7BitEncodedInt();
			reader.Read7BitEncodedInt();
		}
		int num25 = reader.Read7BitEncodedInt();
		for (int num26 = 0; num26 < num25; num26++)
		{
			reader.Read7BitEncodedInt();
		}
		reader.Read7BitEncodedInt();
		int num27 = reader.Read7BitEncodedInt();
		for (int num28 = 0; num28 < num27; num28++)
		{
			string text3 = StringTable[reader.ReadUInt16()];
		}
	}

	private string GetTypeName(int id)
	{
		if (((uint)id & 0x8000u) != 0)
		{
			return XbfFrameworkTypes.GetNameForTypeID(id & -32769) ?? $"UnknownType0x{id:X4}";
		}
		XbfType xbfType = TypeTable[id];
		if (xbfType.Namespace.Name.StartsWith("using:") && _namespacePrefixes.ContainsKey(xbfType.Namespace.Name))
		{
			return _namespacePrefixes[xbfType.Namespace.Name] + ":" + xbfType.Name;
		}
		return xbfType.Name;
	}

	private string GetMarkupTypeName(int id)
	{
		if (((uint)id & 0x8000u) != 0)
		{
			return XbfFrameworkTypes.GetNameForTypeID(id & -32769) ?? $"UnknownType0x{id:X4}";
		}
		XbfType xbfType = TypeTable[id];
		return xbfType.Name;
	}

	private string GetPropertyName(int id)
	{
		if (((uint)id & 0x8000u) != 0)
		{
			return XbfFrameworkTypes.GetNameForPropertyID(id & -32769) ?? $"UnknownProperty0x{id:X4}";
		}
		return PropertyTable[id].Name;
	}

	private string GetEnumerationValue(int enumID, int enumValue)
	{
		return XbfFrameworkTypes.GetNameForEnumValue(enumID, enumValue) ?? $"(Enum0x{enumID:X4}){enumValue}";
	}

	private object GetPropertyValue(BinaryReader reader)
	{
		byte b = reader.ReadByte();
		switch (b)
		{
		case 1:
			return false;
		case 2:
			return true;
		case 3:
			return reader.ReadSingle();
		case 4:
			return reader.ReadInt32();
		case 5:
			return StringTable[reader.ReadUInt16()];
		case 6:
		{
			float num3 = reader.ReadSingle();
			float num4 = reader.ReadSingle();
			float num5 = reader.ReadSingle();
			float num6 = reader.ReadSingle();
			if (num3 == num5 && num4 == num6)
			{
				if (num3 == num4)
				{
					return num3;
				}
				return $"{num3},{num4}";
			}
			return $"{num3},{num4},{num5},{num6}";
		}
		case 7:
		{
			int num = reader.ReadInt32();
			float num2 = reader.ReadSingle();
			return num switch
			{
				0 => "Auto", 
				1 => num2, 
				2 => (num2 == 1f) ? "*" : (num2 + "*"), 
				_ => throw new InvalidDataException("Unexpected value"), 
			};
		}
		case 8:
		{
			byte b2 = reader.ReadByte();
			byte g = reader.ReadByte();
			byte r = reader.ReadByte();
			byte a = reader.ReadByte();
			return Color.FromArgb(a, r, g, b2);
		}
		case 9:
			return ReadString(reader);
		case 11:
		{
			int enumID = reader.ReadUInt16();
			int enumValue = reader.ReadInt32();
			return GetEnumerationValue(enumID, enumValue);
		}
		default:
			throw new InvalidDataException($"Unrecognized value type 0x{b:X2}");
		}
	}
}
