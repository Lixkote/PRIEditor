using System;
using System.IO;
using System.Text;

namespace XbfPriFormat;

public abstract class Section
{
	protected PriFile PriFile { get; private set; }

	public string SectionIdentifier { get; private set; }

	public uint SectionQualifier { get; private set; }

	public uint Flags { get; private set; }

	public uint SectionFlags { get; private set; }

	public uint SectionLength { get; private set; }

	protected Section(string sectionIdentifier, PriFile priFile)
	{
		if (sectionIdentifier.Length != 16)
		{
			throw new ArgumentException("Section identifiers need to be exactly 16 characters long.", "sectionIdentifier");
		}
		SectionIdentifier = sectionIdentifier;
		PriFile = priFile;
	}

	internal bool Parse(BinaryReader binaryReader)
	{
		if (new string(binaryReader.ReadChars(16)) != SectionIdentifier)
		{
			throw new InvalidDataException("Unexpected section identifier.");
		}
		SectionQualifier = binaryReader.ReadUInt32();
		Flags = binaryReader.ReadUInt16();
		SectionFlags = binaryReader.ReadUInt16();
		SectionLength = binaryReader.ReadUInt32();
		binaryReader.ExpectUInt32(0u);
		binaryReader.BaseStream.Seek(SectionLength - 16 - 24, SeekOrigin.Current);
		binaryReader.ExpectUInt32(3740662494u);
		binaryReader.ExpectUInt32(SectionLength);
		binaryReader.BaseStream.Seek(-8 - (SectionLength - 16 - 24), SeekOrigin.Current);
		using SubStream input = new SubStream(binaryReader.BaseStream, binaryReader.BaseStream.Position, (int)(SectionLength - 16 - 24));
		using BinaryReader binaryReader2 = new BinaryReader(input, Encoding.ASCII);
		return ParseSectionContent(binaryReader2);
	}

	protected abstract bool ParseSectionContent(BinaryReader binaryReader);
    protected abstract bool SaveSectionContent(BinaryWriter binaryWriter);

    public override string ToString()
	{
		return $"{SectionIdentifier.TrimEnd('\0', ' ')} length: {SectionLength}";
	}

    public void Save(BinaryWriter binaryWriter)
    {
        // Write the section identifier
        binaryWriter.Write(Encoding.ASCII.GetBytes(SectionIdentifier));

        // Write the section qualifier, flags, section flags, and section length
        binaryWriter.Write(SectionQualifier);
        binaryWriter.Write((ushort)Flags);
        binaryWriter.Write((ushort)SectionFlags);
        binaryWriter.Write(SectionLength);
        binaryWriter.Write((uint)0);

        // Save the current position to calculate the section length later
        long startOffset = binaryWriter.BaseStream.Position;

        // Placeholder for the section content
        binaryWriter.Write(new byte[SectionLength - 16 - 24]);

        // Save the section content
        using (SubStream output = new SubStream(binaryWriter.BaseStream, startOffset, (int)(SectionLength - 16 - 24)))
        {
            // Check if the stream is writable
            if (output.CanWrite)
            {
                using (BinaryWriter contentWriter = new BinaryWriter(output, Encoding.ASCII))
                {
                    // Ensure that SaveSectionContent writes within the available space
                    SaveSectionContent(contentWriter);
                }
            }
            else
            {
                // Handle the case where the stream is not writable
                Console.WriteLine("The stream is not writable.");
            }
        }

        // Calculate the section length and update the placeholder
        long endOffset = binaryWriter.BaseStream.Position;
        binaryWriter.BaseStream.Seek(startOffset - 4, SeekOrigin.Begin);
        binaryWriter.Write((uint)(endOffset - startOffset));

        // Move to the end of the section
        binaryWriter.BaseStream.Seek(endOffset, SeekOrigin.Begin);
    }

    internal static Section CreateForIdentifier(string sectionIdentifier, PriFile priFile)
	{
		return sectionIdentifier switch
		{
			"[mrm_pridescex]\0" => new PriDescriptorSection(priFile), 
			"[mrm_hschema]  \0" => new HierarchicalSchemaSection(priFile, extendedVersion: false), 
			"[mrm_hschemaex] " => new HierarchicalSchemaSection(priFile, extendedVersion: true), 
			"[mrm_decn_info]\0" => new DecisionInfoSection(priFile), 
			"[mrm_res_map__]\0" => new ResourceMapSection(priFile, version2: false), 
			"[mrm_res_map2_]\0" => new ResourceMapSection(priFile, version2: true), 
			"[mrm_dataitem] \0" => new DataItemSection(priFile), 
			"[mrm_rev_map]  \0" => new ReverseMapSection(priFile), 
			"[def_file_list]\0" => new ReferencedFileSection(priFile), 
			_ => new UnknownSection(sectionIdentifier, priFile), 
		};
	}
}
