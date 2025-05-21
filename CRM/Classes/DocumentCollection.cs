using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace CRM.Classes
{
    [Serializable]
    [XmlRoot("DocumentCollection")]
    public class DocumentCollection
    {
        private const string _fileIncoming = "IncomingLettersCollection.xml";
        private const string _fileOutgoing = "OutgoingLettersCollection.xml";
        private const string _fileKD = "KDCollection.xml";

        [XmlElement("Document")]
        public List<SaveDocument> Documents { get; private set; }

        public DocumentCollection()
        {
            Documents = new List<SaveDocument>();

        }

        public void Save(DocumentType docType)
        {
            var serializer = new XmlSerializer(typeof(DocumentCollection));
            using (var writer = new StreamWriter(GetFileName(docType)))
            {
                serializer.Serialize(writer, this);
            }
        }

        public void AddUpdate(SaveDocument newDocument)
        {
            if (newDocument.ID.HasValue)
            {
                SaveDocument oldDocument = Documents.FirstOrDefault(doc => doc.ID == newDocument.ID);
                int index = Documents.IndexOf(oldDocument);
                Documents[index] = newDocument;
            }
            else
            {
                newDocument.ID = Guid.NewGuid();
                Documents.Add(newDocument);
            }

            Save(newDocument.DocumentType);
        }

        public static DocumentCollection GetCollection(DocumentType docType)
        {
            //return new DocumentCollection();
            var serializer = new XmlSerializer(typeof(DocumentCollection));

            using (FileStream fs = new FileStream(GetFileName(docType), FileMode.OpenOrCreate))
            {
                return (DocumentCollection)serializer.Deserialize(fs);
            }
        }
        private static string GetFileName(DocumentType docType)
        {
            if (docType == DocumentType.Incoming)
            {
                return _fileIncoming;
            }
            if (docType == DocumentType.Outgoing)
            {
                return _fileOutgoing;
            }
            else
            {
                return _fileKD;
            }
        }
    }
}