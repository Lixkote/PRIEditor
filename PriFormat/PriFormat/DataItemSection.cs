using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PriFormat;

public class DataItemSection : Section
{
	internal const string Identifier = "[mrm_dataitem] \0";

	public IReadOnlyList<ByteSpan> DataItems { get; private set; }

	internal DataItemSection(PriFile priFile)
		: base("[mrm_dataitem] \0", priFile)
	{
	}

	protected override bool ParseSectionContent(BinaryReader binaryReader)
	{
		long num = (binaryReader.BaseStream as SubStream)?.SubStreamPosition ?? 0;
		binaryReader.ExpectUInt32(0u);
		ushort num2 = binaryReader.ReadUInt16();
		ushort num3 = binaryReader.ReadUInt16();
		uint num4 = binaryReader.ReadUInt32();
		List<ByteSpan> list = new List<ByteSpan>(num2 + num3);
		long num5 = binaryReader.BaseStream.Position + num2 * 2 * 2 + num3 * 2 * 4;
		for (int i = 0; i < num2; i++)
		{
			ushort num6 = binaryReader.ReadUInt16();
			ushort length = binaryReader.ReadUInt16();
			list.Add(new ByteSpan(num + num5 + num6, length));
		}
		for (int j = 0; j < num3; j++)
		{
			uint num7 = binaryReader.ReadUInt32();
			uint length2 = binaryReader.ReadUInt32();
			list.Add(new ByteSpan(num + num5 + num7, length2));
		}
		DataItems = list;
		return true;
	}
    protected override bool SaveSectionContent(BinaryWriter binaryWriter)
    {
        // Write the placeholder for the section content size
        long contentSizePosition = binaryWriter.BaseStream.Position;
        binaryWriter.Write((uint)0);

        // Write the counts of short and long byte spans
        binaryWriter.Write((ushort)DataItems.Count(item => item.Length <= ushort.MaxValue));
        binaryWriter.Write((ushort)DataItems.Count(item => item.Length > ushort.MaxValue));

        // Calculate the total size of the byte spans
        long totalSize = DataItems.Count * (2 * 2 + 4);
        foreach (var byteSpan in DataItems)
        {
            totalSize += byteSpan.Length;
        }

        // Write the total size of the byte spans
        binaryWriter.Write((uint)totalSize);

        // Write short byte spans
        foreach (var byteSpan in DataItems.Where(item => item.Length <= ushort.MaxValue))
        {
            binaryWriter.Write((ushort)byteSpan.Offset);
            binaryWriter.Write((ushort)byteSpan.Length);
        }

        // Write long byte spans
        foreach (var byteSpan in DataItems.Where(item => item.Length > ushort.MaxValue))
        {
            binaryWriter.Write((uint)byteSpan.Offset);
            binaryWriter.Write((uint)byteSpan.Length);
        }

        // Move back to the placeholder and update it with the actual content size
        long endPosition = binaryWriter.BaseStream.Position;
        binaryWriter.BaseStream.Seek(contentSizePosition, SeekOrigin.Begin);
        binaryWriter.Write((uint)(endPosition - contentSizePosition - sizeof(uint)));
        binaryWriter.BaseStream.Seek(endPosition, SeekOrigin.Begin);
        return true;
    }
}
