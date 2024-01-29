using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PriFormat;

public class ReferencedFileSection : Section
{
	private struct FolderInfo
	{
		public ushort ParentFolder;

		public ushort NumFoldersInFolder;

		public ushort FirstFolderInFolder;

		public ushort NumFilesInFolder;

		public ushort FirstFileInFolder;

		public ushort FolderNameLength;

		public ushort FullPathLength;

		public uint FolderNameOffset;

		public FolderInfo(ushort parentFolder, ushort numFoldersInFolder, ushort firstFolderInFolder, ushort numFilesInFolder, ushort firstFileInFolder, ushort folderNameLength, ushort fullPathLength, uint folderNameOffset)
		{
			ParentFolder = parentFolder;
			NumFoldersInFolder = numFoldersInFolder;
			FirstFolderInFolder = firstFolderInFolder;
			NumFilesInFolder = numFilesInFolder;
			FirstFileInFolder = firstFileInFolder;
			FolderNameLength = folderNameLength;
			FullPathLength = fullPathLength;
			FolderNameOffset = folderNameOffset;
		}
	}

	private struct FileInfo
	{
		public ushort ParentFolder;

		public ushort FullPathLength;

		public ushort FileNameLength;

		public uint FileNameOffset;

		public FileInfo(ushort parentFolder, ushort fullPathLength, ushort fileNameLength, uint fileNameOffset)
		{
			ParentFolder = parentFolder;
			FullPathLength = fullPathLength;
			FileNameLength = fileNameLength;
			FileNameOffset = fileNameOffset;
		}
	}

	internal const string Identifier = "[def_file_list]\0";
    private List<ReferencedEntry> list5;
    private ushort num;
    private ushort num2;
    private ushort num3;
    private uint num4;
    private List<FolderInfo> list;

    public IReadOnlyList<ReferencedFile> ReferencedFiles { get; private set; }
    public ushort parentFolder { get; private set; }

    private ushort numFoldersInFolder;
    private ushort firstFolderInFolder;
    private ushort numFilesInFolder;
    private ushort firstFileInFolder;
    private ushort folderNameLength;
    private ushort fullPathLength;
    private uint folderNameOffset;
    private ushort parentFolder2;
    private ushort fullPathLength2;
    private ushort fileNameLength;
    private uint fileNameOffset;
    private long position;
    private List<ReferencedFolder> list3;
    private string name;
    private string name2;
    private ReferencedFolder parent;
    private List<FileInfo> list2;
    private List<ReferencedFile> list4;

    internal ReferencedFileSection(PriFile priFile)
		: base("[def_file_list]\0", priFile)
	{
	}

    protected override bool SaveSectionContent(BinaryWriter binaryWriter)
    {
        // Write num2, num3, and other required information
        binaryWriter.Write((ushort)num2);
        binaryWriter.Write((ushort)num3);
        binaryWriter.Write((ushort)list.Count);
        binaryWriter.Write((ushort)0);
        binaryWriter.Write((uint)num4);

        // Write folder information
        foreach (var folderInfo in list)
        {
            binaryWriter.Write((ushort)0);
            binaryWriter.Write(folderInfo.ParentFolder);
            binaryWriter.Write(folderInfo.NumFoldersInFolder);
            binaryWriter.Write(folderInfo.FirstFolderInFolder);
            binaryWriter.Write(folderInfo.NumFilesInFolder);
            binaryWriter.Write(folderInfo.FirstFileInFolder);
            binaryWriter.Write(folderInfo.FolderNameLength);
            binaryWriter.Write(folderInfo.FullPathLength);
            binaryWriter.Write(folderInfo.FolderNameOffset);
        }

        // Write file information
        foreach (var fileInfo in list2)
        {
            binaryWriter.Write((ushort)0);
            binaryWriter.Write(fileInfo.ParentFolder);
            binaryWriter.Write(fileInfo.FullPathLength);
            binaryWriter.Write(fileInfo.FileNameLength);
            binaryWriter.Write(fileInfo.FileNameOffset);
        }

        long position = binaryWriter.BaseStream.Position;

        foreach (var fileInfo in list2)
        {
            binaryWriter.Write((ushort)0);
            binaryWriter.Write(fileInfo.ParentFolder);
            binaryWriter.Write(fileInfo.FullPathLength);
            binaryWriter.Write(fileInfo.FileNameLength);
            binaryWriter.Write(fileInfo.FileNameOffset);
        }

        foreach (var referencedFolder in list3)
        {
            string folderName = referencedFolder.Name ?? string.Empty;
            byte[] folderNameBytes = Encoding.Unicode.GetBytes(folderName);
            binaryWriter.Write(folderNameBytes);
        }

        foreach (var referencedFile in list4)
        {
            string fileName = referencedFile.Name ?? string.Empty;
            byte[] fileNameBytes = Encoding.Unicode.GetBytes(fileName);
            binaryWriter.Write(fileNameBytes);
        }

        foreach (var referencedFolder in list3)
        {
            ushort parentFolderIndex = (ushort)(referencedFolder.Parent != null ? list3.IndexOf(referencedFolder.Parent) : ushort.MaxValue);
            binaryWriter.Write(parentFolderIndex);
        }

        foreach (var referencedFile in list4)
        {
            ushort parentFolderIndex = (ushort)(referencedFile.Parent != null ? list3.IndexOf(referencedFile.Parent) : ushort.MaxValue);
            binaryWriter.Write(parentFolderIndex);
        }

        foreach (ReferencedFolder referencedFolder in list3)
        {
            List<ReferencedEntry> children = new List<ReferencedEntry>(referencedFolder.Children.Count);
            foreach (ReferencedEntry child in referencedFolder.Children)
            {
                children.Add(child);
            }

            foreach (ReferencedEntry child in children)
            {
                // Check the type of ReferencedEntry
                if (child is ReferencedFolder)
                {
                    binaryWriter.Write("Folder"); // Some identifier for Folder
                    binaryWriter.Write(child.FullName);
                }
                else if (child is ReferencedFile)
                {
                    binaryWriter.Write("File"); // Some identifier for File
                    binaryWriter.Write(child.FullName);
                }
            }
        }
        return true;
    }

    protected override bool ParseSectionContent(BinaryReader binaryReader)
	{
		num = binaryReader.ReadUInt16();
		num2 = binaryReader.ReadUInt16();
		num3 = binaryReader.ReadUInt16();
		binaryReader.ExpectUInt16(0);
		num4 = binaryReader.ReadUInt32();
		list = new List<FolderInfo>(num2);
		for (int i = 0; i < num2; i++)
		{
			binaryReader.ExpectUInt16(0);
			parentFolder = binaryReader.ReadUInt16();
			numFoldersInFolder = binaryReader.ReadUInt16();
			firstFolderInFolder = binaryReader.ReadUInt16();
			numFilesInFolder = binaryReader.ReadUInt16();
			firstFileInFolder = binaryReader.ReadUInt16();
			folderNameLength = binaryReader.ReadUInt16();
			fullPathLength = binaryReader.ReadUInt16();
			folderNameOffset = binaryReader.ReadUInt32();
			list.Add(new FolderInfo(parentFolder, numFoldersInFolder, firstFolderInFolder, numFilesInFolder, firstFileInFolder, folderNameLength, fullPathLength, folderNameOffset));
		}
		list2 = new List<FileInfo>(num3);
		for (int j = 0; j < num3; j++)
		{
			binaryReader.ReadUInt16();
			parentFolder2 = binaryReader.ReadUInt16();
			fullPathLength2 = binaryReader.ReadUInt16();
			fileNameLength = binaryReader.ReadUInt16();
			fileNameOffset = binaryReader.ReadUInt32();
			list2.Add(new FileInfo(parentFolder2, fullPathLength2, fileNameLength, fileNameOffset));
		}
		position = binaryReader.BaseStream.Position;
		list3 = new List<ReferencedFolder>(num2);
		for (int k = 0; k < num2; k++)
		{
			binaryReader.BaseStream.Seek(position + list[k].FolderNameOffset * 2, SeekOrigin.Begin);
			name = binaryReader.ReadString(Encoding.Unicode, list[k].FolderNameLength);
			list3.Add(new ReferencedFolder(null, name));
		}
		for (int l = 0; l < num2; l++)
		{
			if (list[l].ParentFolder != ushort.MaxValue)
			{
				list3[l].Parent = list3[list[l].ParentFolder];
			}
		}
		list4 = new List<ReferencedFile>(num3);
		for (int m = 0; m < num3; m++)
		{
			binaryReader.BaseStream.Seek(position + list2[m].FileNameOffset * 2, SeekOrigin.Begin);
			name2 = binaryReader.ReadString(Encoding.Unicode, list2[m].FileNameLength);
			parent = ((list2[m].ParentFolder == ushort.MaxValue) ? null : list3[list2[m].ParentFolder]);
			list4.Add(new ReferencedFile(parent, name2));
		}
		for (int n = 0; n < num2; n++)
		{
			list5 = new List<ReferencedEntry>(list[n].NumFoldersInFolder + list[n].NumFilesInFolder);
			for (int num5 = 0; num5 < list[n].NumFoldersInFolder; num5++)
			{
				list5.Add(list3[list[n].FirstFolderInFolder + num5]);
			}
			for (int num6 = 0; num6 < list[n].NumFilesInFolder; num6++)
			{
				list5.Add(list4[list[n].FirstFileInFolder + num6]);
			}
			list3[n].Children = list5;
		}
		ReferencedFiles = list4;
		return true;
	}
}
