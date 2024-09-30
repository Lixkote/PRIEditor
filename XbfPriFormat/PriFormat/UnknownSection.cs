using System.IO;

namespace XbfPriFormat;

public class UnknownSection : Section
{
	public byte[] SectionContent { get; private set; }

	internal UnknownSection(string sectionIdentifier, PriFile priFile)
		: base(sectionIdentifier, priFile)
	{
	}

	protected override bool ParseSectionContent(BinaryReader binaryReader)
	{
		int count = (int)(binaryReader.BaseStream.Length - binaryReader.BaseStream.Position);
		SectionContent = binaryReader.ReadBytes(count);
		return true;
	}

    protected override bool SaveSectionContent(BinaryWriter binaryWriter)
    {
        // Assuming SectionContent is a byte array 
        if (SectionContent != null && SectionContent.Length > 0)
        {
            binaryWriter.Write(SectionContent);
        }

        return true;
    }

}
