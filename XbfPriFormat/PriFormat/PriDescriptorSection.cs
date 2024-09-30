using System.Collections.Generic;
using System.IO;

namespace XbfPriFormat;

public class PriDescriptorSection : Section
{
    internal const string Identifier = "[mrm_pridescex]\0";

    public PriDescriptorFlags PriFlags { get; private set; }
    public IReadOnlyList<SectionRef<HierarchicalSchemaSection>> HierarchicalSchemaSections { get; private set; }
    public IReadOnlyList<SectionRef<DecisionInfoSection>> DecisionInfoSections { get; private set; }
    public IReadOnlyList<SectionRef<ResourceMapSection>> ResourceMapSections { get; private set; }
    public IReadOnlyList<SectionRef<ReferencedFileSection>> ReferencedFileSections { get; private set; }
    public IReadOnlyList<SectionRef<DataItemSection>> DataItemSections { get; private set; }
    public SectionRef<ResourceMapSection>? PrimaryResourceMapSection { get; private set; }

    internal PriDescriptorSection(PriFile priFile)
        : base("[mrm_pridescex]\0", priFile)
    {
    }

    protected override bool SaveSectionContent(BinaryWriter binaryWriter)
    {
        // Placeholder for the section content size
        long contentSizePosition = binaryWriter.BaseStream.Position;
        binaryWriter.Write((uint)0);

        // Write the section header
        binaryWriter.Write((ushort)PriFlags);
        binaryWriter.Write((ushort)HierarchicalSchemaSections.Count);
        binaryWriter.Write((ushort)0);
        binaryWriter.Write((ushort)DecisionInfoSections.Count);
        binaryWriter.Write((ushort)ResourceMapSections.Count);
        binaryWriter.Write((ushort)ReferencedFileSections.Count);

        // Write PrimaryResourceMapSection
        if (PrimaryResourceMapSection.HasValue)
        {
            binaryWriter.Write((ushort)PrimaryResourceMapSection.Value.Index);
        }
        else
        {
            binaryWriter.Write(ushort.MaxValue);
        }

        binaryWriter.Write((ushort)DataItemSections.Count);

        // Write section references
        WriteSectionRefs(binaryWriter, HierarchicalSchemaSections);
        WriteSectionRefs(binaryWriter, DecisionInfoSections);
        WriteSectionRefs(binaryWriter, ResourceMapSections);
        WriteSectionRefs(binaryWriter, ReferencedFileSections);
        WriteSectionRefs(binaryWriter, DataItemSections);

        // Move back to the placeholder and update it with the actual content size
        long endPosition = binaryWriter.BaseStream.Position;
        binaryWriter.BaseStream.Seek(contentSizePosition, SeekOrigin.Begin);
        binaryWriter.Write((uint)(endPosition - contentSizePosition - sizeof(uint)));
        binaryWriter.BaseStream.Seek(endPosition, SeekOrigin.Begin);
        return true;
    }

    private void WriteSectionRefs<T>(BinaryWriter binaryWriter, IReadOnlyList<SectionRef<T>> sectionRefs)
        where T : Section
    {
        foreach (var sectionRef in sectionRefs)
        {
            binaryWriter.Write((ushort)sectionRef.Index);
        }
    }

    protected override bool ParseSectionContent(BinaryReader binaryReader)
    {
        PriFlags = (PriDescriptorFlags)binaryReader.ReadUInt16();
        ushort num = binaryReader.ReadUInt16();
        binaryReader.ExpectUInt16(0);
        ushort num2 = binaryReader.ReadUInt16();
        ushort num3 = binaryReader.ReadUInt16();
        ushort num4 = binaryReader.ReadUInt16();
        ushort num5 = binaryReader.ReadUInt16();
        if (num5 != ushort.MaxValue)
        {
            PrimaryResourceMapSection = new SectionRef<ResourceMapSection>(num5);
        }
        else
        {
            PrimaryResourceMapSection = null;
        }
        ushort num6 = binaryReader.ReadUInt16();
        ushort num7 = binaryReader.ReadUInt16();
        binaryReader.ExpectUInt16(0);
        List<SectionRef<HierarchicalSchemaSection>> list = new List<SectionRef<HierarchicalSchemaSection>>(num2);
        for (int i = 0; i < num2; i++)
        {
            list.Add(new SectionRef<HierarchicalSchemaSection>(binaryReader.ReadUInt16()));
        }
        HierarchicalSchemaSections = list;
        List<SectionRef<DecisionInfoSection>> list2 = new List<SectionRef<DecisionInfoSection>>(num3);
        for (int j = 0; j < num3; j++)
        {
            list2.Add(new SectionRef<DecisionInfoSection>(binaryReader.ReadUInt16()));
        }
        DecisionInfoSections = list2;
        List<SectionRef<ResourceMapSection>> list3 = new List<SectionRef<ResourceMapSection>>(num4);
        for (int k = 0; k < num4; k++)
        {
            list3.Add(new SectionRef<ResourceMapSection>(binaryReader.ReadUInt16()));
        }
        ResourceMapSections = list3;
        List<SectionRef<ReferencedFileSection>> list4 = new List<SectionRef<ReferencedFileSection>>(num6);
        for (int l = 0; l < num6; l++)
        {
            list4.Add(new SectionRef<ReferencedFileSection>(binaryReader.ReadUInt16()));
        }
        ReferencedFileSections = list4;
        List<SectionRef<DataItemSection>> list5 = new List<SectionRef<DataItemSection>>(num7);
        for (int m = 0; m < num7; m++)
        {
            list5.Add(new SectionRef<DataItemSection>(binaryReader.ReadUInt16()));
        }
        DataItemSections = list5;
        return true;
    }

    private IReadOnlyList<SectionRef<T>> ParseSectionRefs<T>(BinaryReader binaryReader, ushort count)
        where T : Section
    {
        List<SectionRef<T>> sectionRefs = new List<SectionRef<T>>(count);
        for (int i = 0; i < count; i++)
        {
            sectionRefs.Add(new SectionRef<T>(binaryReader.ReadUInt16()));
        }
        return sectionRefs;
    }
}

