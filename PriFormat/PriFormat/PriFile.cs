using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PriFormat;

public class PriFile
{
	private PriDescriptorSection priDescriptorSection;

	public string Version { get; private set; }

	public uint TotalFileSize { get; private set; }

	public IReadOnlyList<TocEntry> TableOfContents { get; private set; }

	public IReadOnlyList<Section> Sections { get; private set; }

	public PriDescriptorSection PriDescriptorSection
	{
		get
		{
			if (priDescriptorSection == null)
			{
				priDescriptorSection = Sections.OfType<PriDescriptorSection>().Single();
			}
			return priDescriptorSection;
		}
	}

	private PriFile()
	{
	}

	public static PriFile Parse(Stream stream)
	{
		PriFile priFile = new PriFile();
		priFile.ParseInternal(stream);
		return priFile;
	}

    public void Save(Stream streamtosaveinto)
    {
        using (BinaryWriter binaryWriter = new BinaryWriter(streamtosaveinto, Encoding.ASCII, leaveOpen: true))
        {
            // Write the PRI file header
            binaryWriter.Write(Encoding.ASCII.GetBytes(Version));
            binaryWriter.Write((ushort)0);
            binaryWriter.Write((ushort)1);
            binaryWriter.Write(TotalFileSize);
            binaryWriter.Write((uint)0); // Placeholder for the offset to the TOC
            binaryWriter.Write((uint)0); // Placeholder for the offset to the sections
            binaryWriter.Write((ushort)TableOfContents.Count);
            binaryWriter.Write(ushort.MaxValue);
            binaryWriter.Write((uint)0);

            // Placeholder for the TOC entries
            long tocOffset = binaryWriter.BaseStream.Position;
            for (int i = 0; i < TableOfContents.Count; i++)
            {
                binaryWriter.Write((uint)0); // Placeholder for the TOC entry offset
            }

            // Write the sections
            long sectionsOffset = binaryWriter.BaseStream.Position;
            foreach (var section in Sections)
            {
                section.Save(binaryWriter);
            }

            // Now that we know the offsets, update the placeholders
            binaryWriter.BaseStream.Seek(tocOffset, SeekOrigin.Begin);
            foreach (var tocEntry in TableOfContents)
            {
                binaryWriter.Write((uint)tocEntry.SectionOffset);
            }

            binaryWriter.BaseStream.Seek(12, SeekOrigin.Begin); // Move to the offset placeholder
            binaryWriter.Write((uint)tocOffset);

            binaryWriter.BaseStream.Seek(16, SeekOrigin.Begin); // Move to the sections offset placeholder
            binaryWriter.Write((uint)sectionsOffset);
        }
    }

    private void ParseInternal(Stream stream)
	{
		using BinaryReader binaryReader = new BinaryReader(stream, Encoding.ASCII, leaveOpen: true);
		long position = binaryReader.BaseStream.Position;
		string text = new string(binaryReader.ReadChars(8));
		switch (text)
		{
		case "mrm_pri0":
		case "mrm_pri1":
		case "mrm_pri2":
		case "mrm_prif":
		{
			Version = text;
			binaryReader.ExpectUInt16(0);
			binaryReader.ExpectUInt16(1);
			TotalFileSize = binaryReader.ReadUInt32();
			uint num = binaryReader.ReadUInt32();
			uint num2 = binaryReader.ReadUInt32();
			ushort num3 = binaryReader.ReadUInt16();
			binaryReader.ExpectUInt16(ushort.MaxValue);
			binaryReader.ExpectUInt32(0u);
			binaryReader.BaseStream.Seek(position + TotalFileSize - 16, SeekOrigin.Begin);
			binaryReader.ExpectUInt32(3741317854u);
			binaryReader.ExpectUInt32(TotalFileSize);
			binaryReader.ExpectString(text);
			binaryReader.BaseStream.Seek(num, SeekOrigin.Begin);
			List<TocEntry> list = new List<TocEntry>(num3);
			for (int i = 0; i < num3; i++)
			{
				list.Add(TocEntry.Parse(binaryReader));
			}
			TableOfContents = list;
			Section[] array = (Section[])(Sections = new Section[num3]);
			bool flag = false;
			bool flag2 = false;
			do
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] == null)
					{
						binaryReader.BaseStream.Seek(num2 + list[j].SectionOffset, SeekOrigin.Begin);
						Section section = Section.CreateForIdentifier(list[j].SectionIdentifier, this);
						if (section.Parse(binaryReader))
						{
							array[j] = section;
							flag = true;
						}
						else
						{
							flag2 = true;
						}
					}
				}
			}
			while (flag2 && flag);
			if (flag2)
			{
				throw new InvalidDataException();
			}
			break;
		}
		default:
			throw new InvalidDataException("Data does not start with a PRI file header.");
		}
	}

	public T GetSectionByRef<T>(SectionRef<T> sectionRef) where T : Section
	{
		return (T)Sections[sectionRef.sectionIndex];
	}

	public ResourceMapItem GetResourceMapItemByRef(ResourceMapItemRef resourceMapItemRef)
	{
		return GetSectionByRef(resourceMapItemRef.schemaSection).Items[resourceMapItemRef.itemIndex];
	}

	public ByteSpan GetDataItemByRef(DataItemRef dataItemRef)
	{
		return GetSectionByRef(dataItemRef.dataItemSection).DataItems[dataItemRef.itemIndex];
	}

	public ReferencedFile GetReferencedFileByRef(ReferencedFileRef referencedFileRef)
	{
		return GetSectionByRef(PriDescriptorSection.ReferencedFileSections.First()).ReferencedFiles[referencedFileRef.fileIndex];
	}
}
