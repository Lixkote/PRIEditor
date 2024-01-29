using Microsoft.Win32;
using PRIExplorer.Views;
using System;
using System.ComponentModel;
using System.IO;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Markup;
using XbfAnalyzer.Xbf;

namespace PRIExplorer.ViewModels
{
    public class XbfPreviewViewModel
    {
        private readonly byte[] originalData;
        private string xaml;
        private string editedXaml;

        public string Xaml
        {
            get => xaml;
            set
            {
                if (xaml != value)
                {
                    xaml = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Xaml)));
                }
            }
        }

        public string EditedXaml
        {
            get => editedXaml;
            set
            {
                if (editedXaml != value)
                {
                    editedXaml = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditedXaml)));
                }
            }
        }

        public RelayCommand SaveXamlCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public XbfPreviewViewModel(byte[] data)
        {
            originalData = data;

            using (MemoryStream stream = new MemoryStream(data))
            {
                XbfReader xbfReader = new XbfReader(stream);
                if (xbfReader.Header.MajorFileVersion != 2)
                {
                    throw new Exception("Only XBF2 files can be decompiled.");
                }
                Xaml = xbfReader.RootObject.ToString();
            }
        }

        private bool SaveXamlCommand_CanExecute()
        {
            return !string.IsNullOrEmpty(EditedXaml);
        }

        private void SaveXamlCommand_Execute()
        {
            byte[] newBinaryXbf = ConvertXamlToXbf(EditedXaml);
        }

        public byte[] ConvertXamlToXbf(string xamlContent)
        {
            if (xamlContent == null)
            {
                // Log or handle the null xamlContent
                throw new ArgumentNullException(nameof(xamlContent), "XAML content cannot be null.");
            }

            using (MemoryStream xbfStream = new MemoryStream())
            {
                using (InMemoryRandomAccessStream xamlStream = new InMemoryRandomAccessStream())
                using (DataWriter xamlWriter = new DataWriter(xamlStream.GetOutputStreamAt(0)))
                {
                    xamlWriter.WriteString(xamlContent);
                    xamlWriter.StoreAsync().GetResults();
                    xamlStream.Seek(0);

                    using (InMemoryRandomAccessStream binaryStream = new InMemoryRandomAccessStream())
                    using (DataWriter binaryWriter = new DataWriter(binaryStream.GetOutputStreamAt(0)))
                    {
                        // Mock implementation of IXamlMetadataProvider
                        MockXamlMetadataProvider metadataProvider = new MockXamlMetadataProvider();

                        // Call the external XamlBinaryWriter.Write method
                        XamlBinaryWriter.Write(
                            new IRandomAccessStream[] { xamlStream },
                            new IRandomAccessStream[] { binaryStream },
                            metadataProvider
                        );

                        binaryWriter.StoreAsync().GetResults();
                        binaryStream.Seek(0);

                        binaryStream.AsStream().CopyTo(xbfStream);

                        return xbfStream.ToArray();
                    }
                }
            }
        }

        // Mock implementation of IXamlMetadataProvider
        public class MockXamlMetadataProvider : IXamlMetadataProvider
        {
            public IXamlType GetXamlType(Type type)
            {
                return null;
            }

            public IXamlType GetXamlType(string fullName)
            {
                return null;
            }

            public XmlnsDefinition[] GetXmlnsDefinitions()
            {
                return null;
            }
            // Implement required methods or properties here
            // You can provide a minimal implementation that satisfies the interface
        }

    }
}
