using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XbfPriFormat;

public class HierarchicalSchemaSection : Section
{
	private struct ScopeAndItemInfo
	{
		public ushort Parent;

		public ushort FullPathLength;

		public bool IsScope;

		public bool NameInAscii;

		public uint NameOffset;

        public ushort Index;

		public ScopeAndItemInfo(ushort parent, ushort fullPathLength, bool isScope, bool nameInAscii, uint nameOffset, ushort index)
		{
			Parent = parent;
			FullPathLength = fullPathLength;
			IsScope = isScope;
			NameInAscii = nameInAscii;
			NameOffset = nameOffset;
			Index = index;
		}
	}

	private struct ScopeExInfo
	{
		public ushort ScopeIndex;

		public ushort ChildCount;

		public ushort FirstChildIndex;

		public ScopeExInfo(ushort scopeIndex, ushort childCount, ushort firstChildIndex)
		{
			ScopeIndex = scopeIndex;
			ChildCount = childCount;
			FirstChildIndex = firstChildIndex;
		}
	}

	private bool extendedVersion;

	internal const string Identifier1 = "[mrm_hschema]  \0";

	internal const string Identifier2 = "[mrm_hschemaex] ";

	public HierarchicalSchemaVersionInfo Version { get; private set; }

	public string UniqueName { get; private set; }

	public string Name { get; private set; }

	public IReadOnlyList<ResourceMapScope> Scopes { get; private set; }

	public IReadOnlyList<ResourceMapItem> Items { get; private set; }

    public ushort num;
    public ushort num2;
    private uint checksum;
    public uint num3;
    public uint num4;
    public ushort num5;
    public uint num6;
    private bool flag;
    private ushort majorVersion;
    private ushort minorVersion;
    private long position;
    private long num7;
    private List<ResourceMapEntry> list3;
    private ushort index3;
    private ushort index4;
    private ushort index2;
    private string name;
    private long offset;
    private List<ScopeAndItemInfo> list;
    private List<ScopeExInfo> list2;
    private ushort[] array;

    internal HierarchicalSchemaSection(PriFile priFile, bool extendedVersion)
		: base(extendedVersion ? "[mrm_hschemaex] " : "[mrm_hschema]  \0", priFile)
	{
		this.extendedVersion = extendedVersion;
	}

    // Helper method for writing null-terminated strings
    public void WriteNullTerminatedString(BinaryWriter writer, string value, Encoding encoding)
    {
        writer.Write((encoding == Encoding.Unicode) ? (short)0 : (byte)0);
    }


    protected override bool SaveSectionContent(BinaryWriter binaryWriter)
    {
        if (binaryWriter.BaseStream.Length == 0)
        {
            // Write logic for handling empty stream
            return true;
        }

        binaryWriter.Write((ushort)1);
        binaryWriter.Write((ushort)num);
        binaryWriter.Write((ushort)num2);
        binaryWriter.Write((ushort)0);

        if (extendedVersion)
        {
            if (flag)
            {
                binaryWriter.Write("[def_hnamesx]  \0".ToCharArray());
            }
            else
            {
                binaryWriter.Write("[def_hnames]   \0".ToCharArray());
            }
        }

        binaryWriter.Write(majorVersion);
        binaryWriter.Write(minorVersion);
        binaryWriter.Write(0u);
        binaryWriter.Write(checksum);
        binaryWriter.Write(num3);
        binaryWriter.Write(num4);

        WriteNullTerminatedString(binaryWriter, UniqueName, Encoding.Unicode);
        WriteNullTerminatedString(binaryWriter, Name, Encoding.Unicode);

        if (UniqueName.Length != num - 1 || Name.Length != num2 - 1)
        {
            throw new InvalidDataException();
        }

        binaryWriter.Write((ushort)0);
        binaryWriter.Write(num5);
        binaryWriter.Write((ushort)0);
        binaryWriter.Write(num3 + num4);
        binaryWriter.Write(num3);
        binaryWriter.Write(num4);
        binaryWriter.Write(num6);
        binaryWriter.Write((uint)0);

        if (flag)
        {
            binaryWriter.Write((uint)0);
        }

        for (int i = 0; i < num3 + num4; i++)
        {
            binaryWriter.Write(list[i].Parent);
            binaryWriter.Write(list[i].FullPathLength);
            binaryWriter.Write((ushort)list[i].Index);
            binaryWriter.Write(list[i].IsScope ? (byte)0x10 : (byte)0);
            binaryWriter.Write(list[i].NameInAscii ? (byte)0x20 : (byte)0);
            binaryWriter.Write((ushort)(list[i].NameOffset & 0xFFFF));
            binaryWriter.Write(list[i].Index);
        }

        for (int j = 0; j < num3; j++)
        {
            binaryWriter.Write(list2[j].ScopeIndex);
            binaryWriter.Write(list2[j].ChildCount);
            binaryWriter.Write(list2[j].FirstChildIndex);
            binaryWriter.Write((ushort)0);
        }

        for (int k = 0; k < num4; k++)
        {
            binaryWriter.Write(array[k]);
        }

        long position = binaryWriter.BaseStream.Position;
        long num7 = position + num6 * 2;

        for (int l = 0; l < num3 + num4; l++)
        {
            long offset = list[l].NameInAscii ? (position + list[l].NameOffset * 2) : (num7 + list[l].NameOffset);
            binaryWriter.BaseStream.Seek(offset, SeekOrigin.Begin);
            // string name = list[l].FullPathLength == 0 ? string.Empty : (list[l].NameInAscii ? WriteNullTerminatedString(binaryWriter, null, Encoding.ASCII) : WriteNullTerminatedString(binaryWriter, null, Encoding.Unicode));
            WriteNullTerminatedString(binaryWriter, name, list[l].NameInAscii ? Encoding.ASCII : Encoding.Unicode);
            binaryWriter.Write(list[l].Index);
        }

        return true;
    }

    private string ReadNullTerminatedString(BinaryReader binaryReader, Encoding encoding)
    {
        List<byte> bytes = new List<byte>();

        byte currentByte;
        while ((currentByte = binaryReader.ReadByte()) != 0)
        {
            bytes.Add(currentByte);
        }

        return encoding.GetString(bytes.ToArray());
    }

    protected override bool ParseSectionContent(BinaryReader binaryReader)
	{
		if (binaryReader.BaseStream.Length == 0)
		{
			Version = null;
			UniqueName = null;
			Name = null;
			Scopes = Array.Empty<ResourceMapScope>();
			Items = Array.Empty<ResourceMapItem>();
			return true;
		}
		binaryReader.ExpectUInt16(1);
		num = binaryReader.ReadUInt16();
		num2 = binaryReader.ReadUInt16();
		binaryReader.ExpectUInt16(0);
		if (extendedVersion)
		{
			string text = new string(binaryReader.ReadChars(16));
			string text2 = text;
			if (!(text2 == "[def_hnamesx]  \0"))
			{
				if (!(text2 == "[def_hnames]   \0"))
				{
					throw new InvalidDataException();
				}
				flag = false;
			}
			else
			{
				flag = true;
			}
		}
		else
		{
			flag = false;
		}
		majorVersion = binaryReader.ReadUInt16();
		minorVersion = binaryReader.ReadUInt16();
		binaryReader.ExpectUInt32(0u);
		checksum = binaryReader.ReadUInt32();
		num3 = binaryReader.ReadUInt32();
		num4 = binaryReader.ReadUInt32();
		Version = new HierarchicalSchemaVersionInfo(majorVersion, minorVersion, checksum, num3, num4);
		UniqueName = binaryReader.ReadNullTerminatedString(Encoding.Unicode);
		Name = binaryReader.ReadNullTerminatedString(Encoding.Unicode);
		if (UniqueName.Length != num - 1 || Name.Length != num2 - 1)
		{
			throw new InvalidDataException();
		}
		binaryReader.ExpectUInt16(0);
		num5 = binaryReader.ReadUInt16();
		binaryReader.ExpectUInt16(0);
		binaryReader.ExpectUInt32(num3 + num4);
		binaryReader.ExpectUInt32(num3);
		binaryReader.ExpectUInt32(num4);
		num6 = binaryReader.ReadUInt32();
		binaryReader.ReadUInt32();
		if (flag)
		{
			binaryReader.ReadUInt32();
		}
		list = new List<ScopeAndItemInfo>((int)(num3 + num4));
		for (int i = 0; i < num3 + num4; i++)
		{
			ushort parent = binaryReader.ReadUInt16();
			ushort fullPathLength = binaryReader.ReadUInt16();
			char c = (char)binaryReader.ReadUInt16();
			byte b = binaryReader.ReadByte();
			byte b2 = binaryReader.ReadByte();
			uint nameOffset = (uint)(binaryReader.ReadUInt16() | ((b2 & 0xF) << 16));
			ushort index = binaryReader.ReadUInt16();
			bool isScope = (b2 & 0x10) != 0;
			bool nameInAscii = (b2 & 0x20) != 0;
			list.Add(new ScopeAndItemInfo(parent, fullPathLength, isScope, nameInAscii, nameOffset, index));
		}
		list2 = new List<ScopeExInfo>((int)num3);
		for (int j = 0; j < num3; j++)
		{
			ushort scopeIndex = binaryReader.ReadUInt16();
			ushort childCount = binaryReader.ReadUInt16();
			ushort firstChildIndex = binaryReader.ReadUInt16();
			binaryReader.ExpectUInt16(0);
			list2.Add(new ScopeExInfo(scopeIndex, childCount, firstChildIndex));
		}
		array = new ushort[num4];
		for (int k = 0; k < num4; k++)
		{
			array[k] = binaryReader.ReadUInt16();
		}
		position = binaryReader.BaseStream.Position;
		num7 = binaryReader.BaseStream.Position + num6 * 2;
		ResourceMapScope[] array2 = new ResourceMapScope[num3];
		ResourceMapItem[] array3 = new ResourceMapItem[num4];
		for (int l = 0; l < num3 + num4; l++)
		{
			offset = ((!list[l].NameInAscii) ? (position + list[l].NameOffset * 2) : (num7 + list[l].NameOffset));
			binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
			name = ((list[l].FullPathLength == 0) ? string.Empty : binaryReader.ReadNullTerminatedString(list[l].NameInAscii ? Encoding.ASCII : Encoding.Unicode));
			index2 = list[l].Index;
			if (list[l].IsScope)
			{
				if (array2[index2] != null)
				{
					throw new InvalidDataException();
				}
				array2[index2] = new ResourceMapScope(index2, null, name);
			}
			else
			{
				if (array3[index2] != null)
				{
					throw new InvalidDataException();
				}
				array3[index2] = new ResourceMapItem(index2, null, name);
			}
		}
		for (int m = 0; m < num3 + num4; m++)
		{
			index3 = list[m].Index;
			index4 = list[list[m].Parent].Index;
			if (index4 == ushort.MaxValue)
			{
				continue;
			}
			if (list[m].IsScope)
			{
				if (index4 != index3)
				{
					array2[index3].Parent = array2[index4];
				}
			}
			else
			{
				array3[index3].Parent = array2[index4];
			}
		}
		for (int n = 0; n < num3; n++)
		{
			list3 = new List<ResourceMapEntry>(list2[n].ChildCount);
			for (int num8 = 0; num8 < list2[n].ChildCount; num8++)
			{
				ScopeAndItemInfo scopeAndItemInfo = list[list2[n].FirstChildIndex + num8];
				if (scopeAndItemInfo.IsScope)
				{
					list3.Add(array2[scopeAndItemInfo.Index]);
				}
				else
				{
					list3.Add(array3[scopeAndItemInfo.Index]);
				}
			}
			array2[n].Children = list3;
		}
		Scopes = array2;
		Items = array3;
		return true;
	}
}
