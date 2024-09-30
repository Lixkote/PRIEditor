using System.Collections.Generic;
using System.IO;
using System.Text;

namespace XbfPriFormat;

public class ResourceMapSection : Section
{
	private struct ItemToItemInfoGroup
	{
		public uint FirstItem;

		public uint ItemInfoGroup;

		public ItemToItemInfoGroup(uint firstItem, uint itemInfoGroup)
		{
			FirstItem = firstItem;
			ItemInfoGroup = itemInfoGroup;
		}
	}

	private struct ItemInfoGroup
	{
		public uint GroupSize;

		public uint FirstItemInfo;

		public ItemInfoGroup(uint groupSize, uint firstItemInfo)
		{
			GroupSize = groupSize;
			FirstItemInfo = firstItemInfo;
		}
	}

	private struct ItemInfo
	{
		public uint Decision;

		public uint FirstCandidate;

		public ItemInfo(uint decision, uint firstCandidate)
		{
			Decision = decision;
			FirstCandidate = firstCandidate;
		}
	}

	private struct CandidateInfo
	{
		public byte Type;

		public ResourceValueType ResourceValueType;

		public ushort SourceFileIndex;

		public ushort DataItemIndex;

		public ushort DataItemSection;

		public ushort DataLength;

		public uint DataOffset;

		public CandidateInfo(ResourceValueType resourceValueType, ushort sourceFileIndex, ushort dataItemIndex, ushort dataItemSection)
		{
			Type = 1;
			ResourceValueType = resourceValueType;
			SourceFileIndex = sourceFileIndex;
			DataItemIndex = dataItemIndex;
			DataItemSection = dataItemSection;
			DataLength = 0;
			DataOffset = 0u;
		}

		public CandidateInfo(ResourceValueType resourceValueType, ushort dataLength, uint dataOffset)
		{
			Type = 0;
			ResourceValueType = resourceValueType;
			SourceFileIndex = 0;
			DataItemIndex = 0;
			DataItemSection = 0;
			DataLength = dataLength;
			DataOffset = dataOffset;
		}
	}

    private long num;
    private ushort num2;
    private ushort num3;
    private bool version2;
    private ushort num4;
    private ushort num5;
    private ushort num6;
    private uint num7;
    private uint num8;
    private uint num9;
    private uint count2;
    private byte[] array;
    private byte[] array2;
    private ushort majorVersion;
    private ushort minorVersion;
    private uint checksum;
    private uint numScopes;
    private uint numItems;
    private ushort num10;
    private uint unknown;
    private uint unknown2;
    private string text;

    private List<ResourceValueType> list;
    private List<ItemToItemInfoGroup> list2;
    private List<ItemInfoGroup> list3;
    private List<ItemInfo> list4;
    private byte[] array3;
    private uint num11;
    private uint num12;
    private uint num13;

    private uint firstItem2;
    private uint itemInfoGroup2;
    private uint groupSize2;
    private uint firstItemInfo2;
    private uint decision2;
    private uint firstCandidate2;

    private List<CandidateInfo> list5;

    private long position;
    private Dictionary<ushort, CandidateSet> dictionary;
    internal const string Identifier1 = "[mrm_res_map__]\0";

	internal const string Identifier2 = "[mrm_res_map2_]\0";

	public HierarchicalSchemaReference HierarchicalSchemaReference { get; private set; }

	public SectionRef<HierarchicalSchemaSection> SchemaSection { get; private set; }

    private ushort count;
    private ushort sourceFileIndex;
    private ushort dataItemIndex;
    private ushort dataItemSection;
    private ushort dataLength;
    private uint dataOffset;
    private ItemToItemInfoGroup itemToItemInfoGroup;
    private ItemInfoGroup itemInfoGroup3;
    private ItemInfo itemInfo;
    private ushort num18;
    private Decision decision3;
    private List<Candidate> list6;
    private ushort num20;
    private CandidateSet value;

    public SectionRef<DecisionInfoSection> DecisionInfoSection { get; private set; }

	public IReadOnlyDictionary<ushort, CandidateSet> CandidateSets { get; private set; }

	internal ResourceMapSection(PriFile priFile, bool version2)
		: base(version2 ? "[mrm_res_map2_]\0" : "[mrm_res_map__]\0", priFile)
	{
		this.version2 = version2;
	}

    protected override bool SaveSectionContent(BinaryWriter binaryWriter)
    {
        binaryWriter.Write((ushort)num2);
        binaryWriter.Write((ushort)num3);
        binaryWriter.Write((ushort)SchemaSection.Index);
        binaryWriter.Write((ushort)count);
        binaryWriter.Write((ushort)DecisionInfoSection.Index);

        binaryWriter.Write((ushort)num4);
        binaryWriter.Write((ushort)num5);
        binaryWriter.Write((ushort)num6);
        binaryWriter.Write(num7);
        binaryWriter.Write(num8);
        binaryWriter.Write(num9);
        binaryWriter.Write(count2);
        if (base.PriFile.GetSectionByRef(DecisionInfoSection) == null)
        {
            return false;
        }
        binaryWriter.Write(array, 0, array.Length);
        binaryWriter.Write(array2, 0, array2.Length);
        if (array2.Length != 0)
        {
            using (MemoryStream stream = new MemoryStream(array2))
            using (BinaryReader binaryReader2 = new BinaryReader(stream, Encoding.Unicode))
            {
                binaryWriter.Write(majorVersion);
                binaryWriter.Write(minorVersion);
                binaryWriter.Write((uint)0); // Writing 0u as an UInt32
                binaryWriter.Write(checksum);
                binaryWriter.Write(numScopes);
                binaryWriter.Write(numItems);

                HierarchicalSchemaVersionInfo versionInfo = new HierarchicalSchemaVersionInfo(majorVersion, minorVersion, checksum, numScopes, numItems);

                binaryWriter.Write(num10);
                binaryWriter.Write((ushort)0); // Writing 0 as an UInt16
                binaryWriter.Write(unknown);
                binaryWriter.Write(unknown2);

                // Writing null-terminated string (assuming text is a string)
                byte[] textBytes = Encoding.Unicode.GetBytes(text);
                binaryWriter.Write(textBytes);
                binaryWriter.Write((ushort)0); // Writing null terminator

                // Assuming HierarchicalSchemaReference is a property of the class
                HierarchicalSchemaReference = new HierarchicalSchemaReference(versionInfo, unknown, unknown2, text);
            }
        }

        foreach (var item in list)
        {
            binaryWriter.Write((uint)4); // Writing 4u as an UInt32
            binaryWriter.Write((uint)item);
        }

        // Writing list2
        foreach (var item in list2)
        {
            binaryWriter.Write(item.FirstItem);
            binaryWriter.Write(item.ItemInfoGroup);
        }

        // Writing list3
        foreach (var item in list3)
        {
            binaryWriter.Write(item.GroupSize);
            binaryWriter.Write(item.FirstItemInfo);
        }

        // Writing list4
        foreach (var item in list4)
        {
            binaryWriter.Write(item.Decision);
            binaryWriter.Write(item.FirstCandidate);
        }

        binaryWriter.Write(array3, 0, array3.Length);

        if (array3.Length != 0)
        {
            using (MemoryStream stream = new MemoryStream(array3))
            using (BinaryReader binaryReader3 = new BinaryReader(stream))
            {
                num11 = binaryReader3.ReadUInt32();
                num12 = binaryReader3.ReadUInt32();
                num13 = binaryReader3.ReadUInt32();

                // Writing list2
                for (int m = 0; m < num11; m++)
                {
                    uint firstItem2 = binaryReader3.ReadUInt32();
                    uint itemInfoGroup2 = binaryReader3.ReadUInt32();
                    list2.Add(new ItemToItemInfoGroup(firstItem2, itemInfoGroup2));
                }

                // Writing list3
                for (int n = 0; n < num12; n++)
                {
                    uint groupSize2 = binaryReader3.ReadUInt32();
                    uint firstItemInfo2 = binaryReader3.ReadUInt32();
                    list3.Add(new ItemInfoGroup(groupSize2, firstItemInfo2));
                }

                // Writing list4
                for (int num14 = 0; num14 < num13; num14++)
                {
                    uint decision2 = binaryReader3.ReadUInt32();
                    uint firstCandidate2 = binaryReader3.ReadUInt32();
                    list4.Add(new ItemInfo(decision2, firstCandidate2));
                }

                if (binaryReader3.BaseStream.Position != binaryReader3.BaseStream.Length)
                {
                    throw new InvalidDataException();
                }
            }
        }
        // Assuming binaryWriter is an instance of BinaryWriter
        // Assuming ResourceValueType, ItemToItemInfoGroup, ItemInfoGroup, CandidateInfo, Candidate, ByteSpan, CandidateSet, ResourceMapItemRef, ReferencedFileRef, and DataItemRef are classes or structs with appropriate properties

        // Writing list5
        foreach (var candidateInfo in list5)
        {
            binaryWriter.Write((byte)(candidateInfo.Type == 1 ? 1 : 0));

            switch (candidateInfo.Type)
            {
                case 1:
                    binaryWriter.Write((byte)list.IndexOf(candidateInfo.ResourceValueType));
                    binaryWriter.Write(candidateInfo.SourceFileIndex);
                    binaryWriter.Write(candidateInfo.DataItemIndex);
                    binaryWriter.Write(candidateInfo.DataItemSection);
                    break;
                case 0:
                    binaryWriter.Write((byte)list.IndexOf(candidateInfo.ResourceValueType));
                    binaryWriter.Write(candidateInfo.DataLength);
                    binaryWriter.Write(candidateInfo.DataOffset);
                    break;
                default:
                    throw new InvalidDataException();
            }
        }

        position = binaryWriter.BaseStream.Position;

        // Writing dictionary
        foreach (var itemToItemInfoGroup in list2)
        {
            var itemInfoGroup3 = (itemToItemInfoGroup.ItemInfoGroup >= list3.Count)
                ? new ItemInfoGroup(1u, (uint)(itemToItemInfoGroup.ItemInfoGroup - list3.Count))
                : list3[(int)itemToItemInfoGroup.ItemInfoGroup];

            for (uint num17 = itemInfoGroup3.FirstItemInfo; num17 < itemInfoGroup3.FirstItemInfo + itemInfoGroup3.GroupSize; num17++)
            {
                var itemInfo = list4[(int)num17];
                var num18 = (ushort)itemInfo.Decision;
                var decision3 = base.PriFile.GetSectionByRef(DecisionInfoSection).Decisions[num18];
                var list6 = new List<Candidate>(decision3.QualifierSets.Count);

                for (int num19 = 0; num19 < decision3.QualifierSets.Count; num19++)
                {
                    var candidateInfo = list5[(int)itemInfo.FirstCandidate + num19];

                    if (candidateInfo.Type == 1)
                    {
                        list6.Add(new Candidate(
                            sourceFile: (candidateInfo.SourceFileIndex != 0)
                                ? new ReferencedFileRef?(new ReferencedFileRef(candidateInfo.SourceFileIndex - 1))
                                : null,
                            qualifierSet: decision3.QualifierSets[num19].Index,
                            type: candidateInfo.ResourceValueType,
                            dataItem: new DataItemRef(new SectionRef<DataItemSection>(candidateInfo.DataItemSection),
                                candidateInfo.DataItemIndex)
                        ));
                    }
                    else if (candidateInfo.Type == 0)
                    {
                        list6.Add(new Candidate(
                            data: new ByteSpan(num + position + candidateInfo.DataOffset, candidateInfo.DataLength),
                            qualifierSet: decision3.QualifierSets[num19].Index,
                            type: candidateInfo.ResourceValueType
                        ));
                    }
                }

                var num20 = (ushort)(itemToItemInfoGroup.FirstItem + (num17 - itemInfoGroup3.FirstItemInfo));
                var value = new CandidateSet(new ResourceMapItemRef(SchemaSection, num20), num18, list6);
                dictionary.Add(num20, value);
            }
        }
        // CandidateSets = dictionary;
        return true;
    }

    protected override bool ParseSectionContent(BinaryReader binaryReader)
	{
		num = (binaryReader.BaseStream as SubStream)?.SubStreamPosition ?? 0;
		num2 = binaryReader.ReadUInt16();
        num3 = binaryReader.ReadUInt16();
		if (!version2)
		{
			if (num2 == 0 || num3 == 0)
			{
				throw new InvalidDataException();
			}
		}
		else if (num2 != 0 || num3 != 0)
		{
			throw new InvalidDataException();
		}
		SchemaSection = new SectionRef<HierarchicalSchemaSection>(binaryReader.ReadUInt16());
		count = binaryReader.ReadUInt16();
		DecisionInfoSection = new SectionRef<DecisionInfoSection>(binaryReader.ReadUInt16());
		num4 = binaryReader.ReadUInt16();
		num5 = binaryReader.ReadUInt16();
		num6 = binaryReader.ReadUInt16();
		num7 = binaryReader.ReadUInt32();
		num8 = binaryReader.ReadUInt32();
		num9 = binaryReader.ReadUInt32();
		count2 = binaryReader.ReadUInt32();
		if (base.PriFile.GetSectionByRef(DecisionInfoSection) == null)
		{
			return false;
		}
		array = binaryReader.ReadBytes(num2);
		array2 = binaryReader.ReadBytes(count);
		if (array2.Length != 0)
		{
			BinaryReader binaryReader2 = new BinaryReader(new MemoryStream(array2, writable: false));
			majorVersion = binaryReader2.ReadUInt16();
			minorVersion = binaryReader2.ReadUInt16();
			binaryReader2.ExpectUInt32(0u);
			checksum = binaryReader2.ReadUInt32();
			numScopes = binaryReader2.ReadUInt32();
			numItems = binaryReader2.ReadUInt32();
			HierarchicalSchemaVersionInfo versionInfo = new HierarchicalSchemaVersionInfo(majorVersion, minorVersion, checksum, numScopes, numItems);
			num10 = binaryReader2.ReadUInt16();
			binaryReader2.ExpectUInt16(0);
			unknown = binaryReader2.ReadUInt32();
			unknown2 = binaryReader2.ReadUInt32();
			text = binaryReader2.ReadNullTerminatedString(Encoding.Unicode);
			if (text.Length != num10 - 1)
			{
				throw new InvalidDataException();
			}
			HierarchicalSchemaReference = new HierarchicalSchemaReference(versionInfo, unknown, unknown2, text);
		}
		list = new List<ResourceValueType>(num4);
		for (int i = 0; i < num4; i++)
		{
			binaryReader.ExpectUInt32(4u);
			ResourceValueType item = (ResourceValueType)binaryReader.ReadUInt32();
			list.Add(item);
		}
		list2 = new List<ItemToItemInfoGroup>();
		for (int j = 0; j < num5; j++)
		{
			ushort firstItem = binaryReader.ReadUInt16();
			ushort itemInfoGroup = binaryReader.ReadUInt16();
			list2.Add(new ItemToItemInfoGroup(firstItem, itemInfoGroup));
		}
		list3 = new List<ItemInfoGroup>();
		for (int k = 0; k < num6; k++)
		{
			ushort groupSize = binaryReader.ReadUInt16();
			ushort firstItemInfo = binaryReader.ReadUInt16();
			list3.Add(new ItemInfoGroup(groupSize, firstItemInfo));
		}
		list4 = new List<ItemInfo>();
		for (int l = 0; l < num7; l++)
		{
			ushort decision = binaryReader.ReadUInt16();
			ushort firstCandidate = binaryReader.ReadUInt16();
			list4.Add(new ItemInfo(decision, firstCandidate));
		}
		array3 = binaryReader.ReadBytes((int)count2);
		if (array3.Length != 0)
		{
			using BinaryReader binaryReader3 = new BinaryReader(new MemoryStream(array3, writable: false));
			num11 = binaryReader3.ReadUInt32();
			num12 = binaryReader3.ReadUInt32();
			num13 = binaryReader3.ReadUInt32();
			for (int m = 0; m < num11; m++)
			{
				firstItem2 = binaryReader3.ReadUInt32();
				itemInfoGroup2 = binaryReader3.ReadUInt32();
				list2.Add(new ItemToItemInfoGroup(firstItem2, itemInfoGroup2));
			}
			for (int n = 0; n < num12; n++)
			{
				groupSize2 = binaryReader3.ReadUInt32();
				firstItemInfo2 = binaryReader3.ReadUInt32();
				list3.Add(new ItemInfoGroup(groupSize2, firstItemInfo2));
			}
			for (int num14 = 0; num14 < num13; num14++)
			{
				decision2 = binaryReader3.ReadUInt32();
				firstCandidate2 = binaryReader3.ReadUInt32();
				list4.Add(new ItemInfo(decision2, firstCandidate2));
			}
			if (binaryReader3.BaseStream.Position != binaryReader3.BaseStream.Length)
			{
				throw new InvalidDataException();
			}
		}
		list5 = new List<CandidateInfo>((int)num8);
		for (int num15 = 0; num15 < num8; num15++)
		{
			switch (binaryReader.ReadByte())
			{
			case 1:
			{
				ResourceValueType resourceValueType2 = list[binaryReader.ReadByte()];
				sourceFileIndex = binaryReader.ReadUInt16();
				dataItemIndex = binaryReader.ReadUInt16();
				dataItemSection = binaryReader.ReadUInt16();
				list5.Add(new CandidateInfo(resourceValueType2, sourceFileIndex, dataItemIndex, dataItemSection));
				break;
			}
			case 0:
			{
				ResourceValueType resourceValueType = list[binaryReader.ReadByte()];
				dataLength = binaryReader.ReadUInt16();
				dataOffset = binaryReader.ReadUInt32();
				list5.Add(new CandidateInfo(resourceValueType, dataLength, dataOffset));
				break;
			}
			default:
				throw new InvalidDataException();
			}
		}
		position = binaryReader.BaseStream.Position;
		dictionary = new Dictionary<ushort, CandidateSet>();
		for (int num16 = 0; num16 < list2.Count; num16++)
		{
			itemToItemInfoGroup = list2[num16];
			itemInfoGroup3 = ((itemToItemInfoGroup.ItemInfoGroup >= list3.Count) ? new ItemInfoGroup(1u, (uint)(itemToItemInfoGroup.ItemInfoGroup - list3.Count)) : list3[(int)itemToItemInfoGroup.ItemInfoGroup]);
			for (uint num17 = itemInfoGroup3.FirstItemInfo; num17 < itemInfoGroup3.FirstItemInfo + itemInfoGroup3.GroupSize; num17++)
			{
				itemInfo = list4[(int)num17];
				num18 = (ushort)itemInfo.Decision;
				decision3 = base.PriFile.GetSectionByRef(DecisionInfoSection).Decisions[num18];
				list6 = new List<Candidate>(decision3.QualifierSets.Count);
				for (int num19 = 0; num19 < decision3.QualifierSets.Count; num19++)
				{
					CandidateInfo candidateInfo = list5[(int)itemInfo.FirstCandidate + num19];
					if (candidateInfo.Type == 1)
					{
						list6.Add(new Candidate(sourceFile: (candidateInfo.SourceFileIndex != 0) ? new ReferencedFileRef?(new ReferencedFileRef(candidateInfo.SourceFileIndex - 1)) : null, qualifierSet: decision3.QualifierSets[num19].Index, type: candidateInfo.ResourceValueType, dataItem: new DataItemRef(new SectionRef<DataItemSection>(candidateInfo.DataItemSection), candidateInfo.DataItemIndex)));
					}
					else if (candidateInfo.Type == 0)
					{
						list6.Add(new Candidate(data: new ByteSpan(num + position + candidateInfo.DataOffset, candidateInfo.DataLength), qualifierSet: decision3.QualifierSets[num19].Index, type: candidateInfo.ResourceValueType));
					}
				}
				num20 = (ushort)(itemToItemInfoGroup.FirstItem + (num17 - itemInfoGroup3.FirstItemInfo));
				value = new CandidateSet(new ResourceMapItemRef(SchemaSection, num20), num18, list6);
				dictionary.Add(num20, value);
			}
		}
		CandidateSets = dictionary;
		return true;
	}
}
