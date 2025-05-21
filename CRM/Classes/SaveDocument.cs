using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CRM.Classes
{
    public class SaveDocument
    {
        public Guid? ID;
        public int ProjectID;
        public int OrganizationID;
        public string Subject;
        public DateTime IncomingDate;
        public string Message;
        public string PathFile;
        public DocumentType DocumentType;
        public SaveDocument(Letter letter)
        {
            ID = letter.ID;
            ProjectID = letter.ProjectID;
            OrganizationID = letter.OrganizationID;
            Subject = letter.Subject;
            IncomingDate = letter.IncomingDate;
            Message = letter.Message;
            PathFile = letter.PathFile;
            DocumentType = letter.DocumentType;
        }

        public SaveDocument()
        {
        }
    }
}
