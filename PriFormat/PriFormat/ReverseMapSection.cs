using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PriFormat;

public class ReverseMapSection : Section
{
	internal const string Identifier = "[mrm_rev_map]  \0";
    private uint num;
    private int item9;
    private bool flag2;
    private int item10;
    private ushort item10a;
    private ushort[] array2;
    private List<Tuple<ushort, ushort, uint, uint, ushort>> list;
    private uint[] array;

    public uint[] Mapping { get; private set; }

    private ushort num2;
    private uint num3;
    private uint num4;
    private uint num5;
    private List<Tuple<ushort, ushort, ushort>> list2;
    private ushort item5;
    private ushort item6;
    private ushort item7;
    private ushort item;
    private ushort item2;
    private uint num6;
    private uint item3;
    private ushort item4;
    private long position;
    private long num7;
    private ResourceMapScope[] array3;
    private bool flag;
    private long offset;
    private string name;
    private ushort item8;
    private ResourceMapItem[] array4;
    private ResourceMapEntry[] array5;

    public IReadOnlyList<ResourceMapScope> Scopes { get; private set; }

	public IReadOnlyList<ResourceMapItem> Items { get; private set; }

	internal ReverseMapSection(PriFile priFile)
		: base("[mrm_rev_map]  \0", priFile)
	{
	}

    // Helper method for writing null-terminated strings
    public void WriteNullTerminatedString(BinaryWriter writer, string value, Encoding encoding)
    {
        writer.Write((encoding == Encoding.Unicode) ? (short)0 : (byte)0);
    }

    protected override bool SaveSectionContent(BinaryWriter binaryWriter)
    {
        // Write the initial information
        binaryWriter.Write((uint)Mapping.Length);
        binaryWriter.Write((uint)(8 + Mapping.Length * 4));

        // Write Mapping array
        foreach (uint value in Mapping)
        {
            binaryWriter.Write(value);
        }

        binaryWriter.Write((ushort)list2.Count);
        binaryWriter.Write((ushort)0);
        binaryWriter.Write(num3);
        binaryWriter.Write(num4);
        binaryWriter.Write((uint)(num4 + Mapping.Length));
        binaryWriter.Write(num5);
        binaryWriter.Write((uint)0);

        // Write list
        foreach (var tuple in list)
        {
            binaryWriter.Write(tuple.Item1);
            binaryWriter.Write(tuple.Item2);
            binaryWriter.Write(tuple.Item3);
            binaryWriter.Write((ushort)(tuple.Item4 & 0xFFFF));
            binaryWriter.Write(tuple.Item5);
        }

        // Write list2
        foreach (var tuple in list2)
        {
            binaryWriter.Write(tuple.Item1);
            binaryWriter.Write(tuple.Item2);
            binaryWriter.Write(tuple.Item3);
            binaryWriter.Write(0);
        }

        // Write array2
        foreach (ushort value in array2)
        {
            binaryWriter.Write(value);
        }

        position = binaryWriter.BaseStream.Position;
        num7 = position + num5 * 2;

        // Write scopes and items
        for (int m = 0; m < num4 + Mapping.Length; m++)
        {
            bool flag = (list[m].Item3 & 0x20000000) != 0;
            long offset = (flag ? num7 : position) + list[m].Item4 * (flag ? 1 : 2);
            binaryWriter.BaseStream.Seek(offset, SeekOrigin.Begin);

            string name = (list[m].Item2 == 0) ? string.Empty : list[m].Item2 == ushort.MaxValue ? string.Empty : array3[list[m].Item5].Name;

            WriteNullTerminatedString(binaryWriter, name, flag ? Encoding.ASCII : Encoding.Unicode);
            binaryWriter.Write(list[m].Item5);
        }

        // Write parent-child relationships
        for (int n = 0; n < num4 + Mapping.Length; n++)
        {
            ushort item9 = list[n].Item5;
            bool flag2 = (list[n].Item3 & 0x10000000) != 0;
            ushort item10 = list[n].Item1;
            item10 = list[item10].Item5;

            if (item10 == ushort.MaxValue)
            {
                continue;
            }

            if (flag2)
            {
                if (item10 != item9)
                {
                    array3[item9].Parent = array3[item10];
                }
            }
            else
            {
                array4[item9].Parent = array3[item10];
            }
        }
        // Assuming binaryWriter is an instance of BinaryWriter
        // Assuming ResourceMapEntry is a class or struct with appropriate properties

        for (int num8 = 0; num8 < num4; num8++)
        {
            ResourceMapEntry[] array5 = new ResourceMapEntry[list2[num8].Item2];

            for (int num9 = 0; num9 < array5.Length; num9++)
            {
                Tuple<ushort, ushort, uint, uint, ushort> tuple = list[list2[num8].Item3 + num9];

                if ((tuple.Item3 & 0x10000000u) != 0)
                {
                    array5[num9] = array3[tuple.Item5];
                }
                else
                {
                    array5[num9] = array4[tuple.Item5];
                }
            }

            // Assuming array3 is an array of ResourceMapEntry
            array3[num8].Children = array5;
        }
        return true;
    }


    protected override bool ParseSectionContent(BinaryReader binaryReader)
	{
		num = binaryReader.ReadUInt32();
		binaryReader.ExpectUInt32((uint)(binaryReader.BaseStream.Length - 8));
		array = new uint[num];
		for (int i = 0; i < num; i++)
		{
			array[i] = binaryReader.ReadUInt32();
		}
		Mapping = array;
		num2 = binaryReader.ReadUInt16();
		binaryReader.ExpectUInt16(0);
		num3 = binaryReader.ReadUInt32();
		num4 = binaryReader.ReadUInt32();
		binaryReader.ExpectUInt32(num);
		num5 = binaryReader.ReadUInt32();
		binaryReader.ReadUInt32();
		list = new List<Tuple<ushort, ushort, uint, uint, ushort>>();
		for (int j = 0; j < num4 + num; j++)
		{
			 item = binaryReader.ReadUInt16();
			 item2 = binaryReader.ReadUInt16();
			 num6 = binaryReader.ReadUInt32();
			 item3 = binaryReader.ReadUInt16() | (((num6 >> 24) & 0xF) << 16);
			 item4 = binaryReader.ReadUInt16();
			list.Add(new Tuple<ushort, ushort, uint, uint, ushort>(item, item2, num6, item3, item4));
		}
		list2 = new List<Tuple<ushort, ushort, ushort>>();
		for (int k = 0; k < num4; k++)
		{
			 item5 = binaryReader.ReadUInt16();
			 item6 = binaryReader.ReadUInt16();
			 item7 = binaryReader.ReadUInt16();
			binaryReader.ExpectUInt16(0);
			list2.Add(new Tuple<ushort, ushort, ushort>(item5, item6, item7));
		}
		array2 = new ushort[num];
		for (int l = 0; l < num; l++)
		{
			array2[l] = binaryReader.ReadUInt16();
		}
		position = binaryReader.BaseStream.Position;
		num7 = binaryReader.BaseStream.Position + num5 * 2;
		array3 = new ResourceMapScope[num4];
		array4 = new ResourceMapItem[num];
		for (int m = 0; m < num4 + num; m++)
		{
			flag = (list[m].Item3 & 0x20000000) != 0;
			offset = (flag ? num7 : position) + list[m].Item4 * (flag ? 1 : 2);
			binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
			name = ((list[m].Item2 == 0) ? string.Empty : binaryReader.ReadNullTerminatedString(flag ? Encoding.ASCII : Encoding.Unicode));
			item8 = list[m].Item5;
			if ((list[m].Item3 & 0x10000000u) != 0)
			{
				if (array3[item8] != null)
				{
					throw new InvalidDataException();
				}
				array3[item8] = new ResourceMapScope(item8, null, name);
			}
			else
			{
				if (array4[item8] != null)
				{
					throw new InvalidDataException();
				}
				array4[item8] = new ResourceMapItem(item8, null, name);
			}
		}
		for (int n = 0; n < num4 + num; n++)
		{
			 item9 = list[n].Item5;
			 flag2 = (list[n].Item3 & 0x10000000) != 0;
			 item10 = list[n].Item1;
			item10a = list[item10].Item5;
			if (item10a == ushort.MaxValue)
			{
				continue;
			}
			if (flag2)
			{
				if (item10 != item9)
				{
					array3[item9].Parent = array3[item10];
				}
			}
			else
			{
				array4[item9].Parent = array3[item10];
			}
		}
		for (int num8 = 0; num8 < num4; num8++)
		{
			array5 = new ResourceMapEntry[list2[num8].Item2];
			for (int num9 = 0; num9 < array5.Length; num9++)
			{
				Tuple<ushort, ushort, uint, uint, ushort> tuple = list[list2[num8].Item3 + num9];
				if ((tuple.Item3 & 0x10000000u) != 0)
				{
					array5[num9] = array3[tuple.Item5];
				}
				else
				{
					array5[num9] = array4[tuple.Item5];
				}
			}
			array3[num8].Children = array5;
		}
		Scopes = array3;
		Items = array4;
		return true;
	}
}
