using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace XbfPriFormat.Pers;

internal class Pers
{
	public List<GifPart> GifParts = new List<GifPart>();

	public Version Version;

	public uint XmlLength;

	private XmlDocument _xml = null;

	public byte[] Data { get; internal set; }

	public byte[] XmlData { get; internal set; }

	public XmlDocument Xml
	{
		get
		{
			if (_xml != null)
			{
				return _xml;
			}
			if (XmlData == null)
			{
				return null;
			}
			_xml = new XmlDocument();
			_xml.Load(new MemoryStream(XmlData));
			return _xml;
		}
	}

	internal void ParseXml()
	{
		if (Xml == null)
		{
			return;
		}
		XmlNode xmlNode = Xml.SelectSingleNode("Xml").SelectSingleNode("Parts");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		foreach (XmlNode item in childNodes)
		{
			try
			{
				GifPart gifPart = new GifPart();
				gifPart.Offset = int.Parse(item["Offset"].InnerText);
				gifPart.Size = int.Parse(item["Size"].InnerText);
				gifPart.Frames = int.Parse(item["Frames"].InnerText);
				GifParts.Add(gifPart);
			}
			catch (NullReferenceException)
			{
			}
		}
	}

	public static bool IsPers(byte[] bytes)
	{
		if (bytes == null || bytes.Length < 4)
		{
			return false;
		}
		return bytes[0] == 80 && bytes[1] == 69 && bytes[2] == 82 && bytes[3] == 83;
	}

	public byte[] Save()
	{
		throw new NotImplementedException();
	}

	public static Pers Read(byte[] bytes)
	{
		Pers pers = new Pers();
		using (MemoryStream input = new MemoryStream(bytes))
		{
			BinaryReader binaryReader = new BinaryReader(input);
			if (binaryReader.ReadChars(4).ToRealString() != "PERS")
			{
				throw new FormatException("not a correct pers file");
			}
			pers.Version = new Version(binaryReader.ReadUInt16(), binaryReader.ReadUInt16());
			pers.XmlLength = binaryReader.ReadUInt32();
			pers.XmlData = binaryReader.ReadBytes((int)pers.XmlLength);
			long position = binaryReader.BaseStream.Position;
			pers.Data = binaryReader.ReadBytes((int)(bytes.Length - pers.XmlLength - 12));
			pers.ParseXml();
			int num = 0;
			foreach (GifPart gifPart in pers.GifParts)
			{
				gifPart.Name = num.ToString();
				num++;
				binaryReader.BaseStream.Seek(position, SeekOrigin.Begin);
				binaryReader.BaseStream.Seek(gifPart.Offset, SeekOrigin.Current);
				gifPart.Data = binaryReader.ReadBytes(gifPart.Size);
			}
		}
		return pers;
	}
}
