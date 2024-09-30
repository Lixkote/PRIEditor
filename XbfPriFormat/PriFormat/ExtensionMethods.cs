using System.IO;
using System.Text;

namespace XbfPriFormat;

internal static class ExtensionMethods
{
	public static string ReadString(this BinaryReader reader, Encoding encoding, int length)
	{
		using BinaryReader binaryReader = new BinaryReader(reader.BaseStream, encoding, leaveOpen: true);
		return new string(binaryReader.ReadChars(length));
	}

	public static string ReadNullTerminatedString(this BinaryReader reader, Encoding encoding)
	{
		using BinaryReader binaryReader = new BinaryReader(reader.BaseStream, encoding, leaveOpen: true);
		StringBuilder stringBuilder = new StringBuilder();
		char value;
		while ((value = binaryReader.ReadChar()) != 0)
		{
			stringBuilder.Append(value);
		}
		return stringBuilder.ToString();
	}

	public static void ExpectByte(this BinaryReader reader, byte expectedValue)
	{
		if (reader.ReadByte() != expectedValue)
		{
			throw new InvalidDataException("Unexpected value read.");
		}
	}

	public static void ExpectUInt16(this BinaryReader reader, ushort expectedValue)
	{
		if (reader.ReadUInt16() != expectedValue)
		{
			throw new InvalidDataException("Unexpected value read.");
		}
	}

	public static void ExpectUInt32(this BinaryReader reader, uint expectedValue)
	{
		if (reader.ReadUInt32() != expectedValue)
		{
			throw new InvalidDataException("Unexpected value read.");
		}
	}

	public static void ExpectString(this BinaryReader reader, string s)
	{
		if (new string(reader.ReadChars(s.Length)) != s)
		{
			throw new InvalidDataException("Unexpected value read.");
		}
	}
}
