using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PriFormat;

public class DecisionInfoSection : Section
{
	private struct DecisionInfo
	{
		public ushort FirstQualifierSetIndexIndex;

		public ushort NumQualifierSetsInDecision;

		public DecisionInfo(ushort firstQualifierSetIndexIndex, ushort numQualifierSetsInDecision)
		{
			FirstQualifierSetIndexIndex = firstQualifierSetIndexIndex;
			NumQualifierSetsInDecision = numQualifierSetsInDecision;
		}
	}

	private struct QualifierSetInfo
	{
		public ushort FirstQualifierIndexIndex;

		public ushort NumQualifiersInSet;

		public QualifierSetInfo(ushort firstQualifierIndexIndex, ushort numQualifiersInSet)
		{
			FirstQualifierIndexIndex = firstQualifierIndexIndex;
			NumQualifiersInSet = numQualifiersInSet;
		}
	}

	private struct QualifierInfo
	{
		public ushort Index;

		public ushort Priority;

		public ushort FallbackScore;

		public QualifierInfo(ushort index, ushort priority, ushort fallbackScore)
		{
			Index = index;
			Priority = priority;
			FallbackScore = fallbackScore;
		}
	}

	private struct DistinctQualifierInfo
	{
		public QualifierType QualifierType;

		public uint OperandValueOffset;

		public DistinctQualifierInfo(QualifierType qualifierType, uint operandValueOffset)
		{
			QualifierType = qualifierType;
			OperandValueOffset = operandValueOffset;
		}
	}

	internal const string Identifier = "[mrm_decn_info]\0";

	public IReadOnlyList<Decision> Decisions { get; private set; }

    public IReadOnlyList<DecisionInfoSection> Items { get; private set; }

    public IReadOnlyList<DecisionInfoSection> Items2 { get; private set; }

    public IReadOnlyList<QualifierSet> QualifierSets { get; private set; }

	public IReadOnlyList<Qualifier> Qualifiers { get; private set; }

	internal DecisionInfoSection(PriFile priFile)
		: base("[mrm_decn_info]\0", priFile)
	{
	}

    protected override bool SaveSectionContent(BinaryWriter binaryWriter)
    {
        // Write the placeholder for the section content size
        long contentSizePosition = binaryWriter.BaseStream.Position;
        binaryWriter.Write((uint)0);

        // Write the counts of ResourceMapItem and ResourceMapItem2
        binaryWriter.Write((ushort)Items.Count);
        binaryWriter.Write((ushort)Items2.Count);

        // Write ResourceMapItem
        foreach (var item in Items)
        {
            item.Save(binaryWriter);
        }

        // Write ResourceMapItem2
        foreach (var item in Items2)
        {
            item.Save(binaryWriter);
        }

        // Move back to the placeholder and update it with the actual content size
        long endPosition = binaryWriter.BaseStream.Position;
        binaryWriter.BaseStream.Seek(contentSizePosition, SeekOrigin.Begin);
        binaryWriter.Write((uint)(endPosition - contentSizePosition - sizeof(uint)));
        binaryWriter.BaseStream.Seek(endPosition, SeekOrigin.Begin);
		return true;
    }

    protected override bool ParseSectionContent(BinaryReader binaryReader)
	{
		ushort num = binaryReader.ReadUInt16();
		ushort num2 = binaryReader.ReadUInt16();
		ushort num3 = binaryReader.ReadUInt16();
		ushort num4 = binaryReader.ReadUInt16();
		ushort num5 = binaryReader.ReadUInt16();
		ushort num6 = binaryReader.ReadUInt16();
		List<DecisionInfo> list = new List<DecisionInfo>(num4);
		for (int i = 0; i < num4; i++)
		{
			ushort firstQualifierSetIndexIndex = binaryReader.ReadUInt16();
			ushort numQualifierSetsInDecision = binaryReader.ReadUInt16();
			list.Add(new DecisionInfo(firstQualifierSetIndexIndex, numQualifierSetsInDecision));
		}
		List<QualifierSetInfo> list2 = new List<QualifierSetInfo>(num3);
		for (int j = 0; j < num3; j++)
		{
			ushort firstQualifierIndexIndex = binaryReader.ReadUInt16();
			ushort numQualifiersInSet = binaryReader.ReadUInt16();
			list2.Add(new QualifierSetInfo(firstQualifierIndexIndex, numQualifiersInSet));
		}
		List<QualifierInfo> list3 = new List<QualifierInfo>(num2);
		for (int k = 0; k < num2; k++)
		{
			ushort index = binaryReader.ReadUInt16();
			ushort priority = binaryReader.ReadUInt16();
			ushort fallbackScore = binaryReader.ReadUInt16();
			binaryReader.ExpectUInt16(0);
			list3.Add(new QualifierInfo(index, priority, fallbackScore));
		}
		List<DistinctQualifierInfo> list4 = new List<DistinctQualifierInfo>(num);
		for (int l = 0; l < num; l++)
		{
			binaryReader.ReadUInt16();
			QualifierType qualifierType = (QualifierType)binaryReader.ReadUInt16();
			binaryReader.ReadUInt16();
			binaryReader.ReadUInt16();
			uint operandValueOffset = binaryReader.ReadUInt32();
			list4.Add(new DistinctQualifierInfo(qualifierType, operandValueOffset));
		}
		ushort[] array = new ushort[num5];
		for (int m = 0; m < num5; m++)
		{
			array[m] = binaryReader.ReadUInt16();
		}
		long position = binaryReader.BaseStream.Position;
		List<Qualifier> list5 = new List<Qualifier>(num2);
		for (int n = 0; n < num2; n++)
		{
			DistinctQualifierInfo distinctQualifierInfo = list4[list3[n].Index];
			binaryReader.BaseStream.Seek(position + distinctQualifierInfo.OperandValueOffset * 2, SeekOrigin.Begin);
			string value = binaryReader.ReadNullTerminatedString(Encoding.Unicode);
			list5.Add(new Qualifier((ushort)n, distinctQualifierInfo.QualifierType, list3[n].Priority, (float)(int)list3[n].FallbackScore / 1000f, value));
		}
		Qualifiers = list5;
		List<QualifierSet> list6 = new List<QualifierSet>(num3);
		for (int num7 = 0; num7 < num3; num7++)
		{
			List<Qualifier> list7 = new List<Qualifier>(list2[num7].NumQualifiersInSet);
			for (int num8 = 0; num8 < list2[num7].NumQualifiersInSet; num8++)
			{
				list7.Add(list5[array[list2[num7].FirstQualifierIndexIndex + num8]]);
			}
			list6.Add(new QualifierSet((ushort)num7, list7));
		}
		QualifierSets = list6;
		List<Decision> list8 = new List<Decision>(num4);
		for (int num9 = 0; num9 < num4; num9++)
		{
			List<QualifierSet> list9 = new List<QualifierSet>(list[num9].NumQualifierSetsInDecision);
			for (int num10 = 0; num10 < list[num9].NumQualifierSetsInDecision; num10++)
			{
				list9.Add(list6[array[list[num9].FirstQualifierSetIndexIndex + num10]]);
			}
			list8.Add(new Decision((ushort)num9, list9));
		}
		Decisions = list8;
        return true;
    }
}
