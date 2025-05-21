using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace CRM.Classes
{
    public class Letter
    {
        public Guid? ID { get; private set; }
        public int ProjectID { get; private set; } = -1;
        public int OrganizationID { get; private set; } = -1;
        public string Subject { get; private set; }
        public string OrganizationName { get; private set; }
        public DateTime IncomingDate { get; private set; }
        public string ProjectName { get; private set; }
        public string Message { get; private set; }
        public string PathFile { get; private set; }
        public DocumentType DocumentType { get; private set; }

        public Letter(string subject, string message, int projectId, int organizationId, Guid? id, string pathFile, DocumentType documentType)
        {
            Subject = subject;
            Message = message;
            IncomingDate = DateTime.Now;
            OrganizationID = organizationId;
            ProjectID = projectId;
            ID = id;
            PathFile = pathFile;
            DocumentType = documentType;
        }

        public Letter(SaveDocument saveLetter)
        {
            Subject = saveLetter.Subject;
            Message = saveLetter.Message;
            IncomingDate = saveLetter.IncomingDate;
            OrganizationID = saveLetter.OrganizationID;
            ProjectID = saveLetter.ProjectID;
            ID = saveLetter.ID;
            PathFile = saveLetter.PathFile;
        }

        public bool CanSave()
        {
            if (string.IsNullOrEmpty(Subject))
            {
                return false;
            }
            if (string.IsNullOrEmpty(Message))
            {
                return false;
            }
            if (ProjectID < 0)
            {
                return false;
            }
            if (ProjectID < 0)
            {
                return false;
            }
            if (OrganizationID < 0)
            {
                return false;
            }
            return true;
        }

        public void Save()
        {
            SaveDocument saveLetter = new SaveDocument(this);
            DocumentCollection incomingLetterCollection = DocumentCollection.GetCollection(DocumentType);

            incomingLetterCollection.AddUpdate(saveLetter);
        }

        public static ObservableCollection<Letter> GetAll(DocumentType docType)
        {
            var saveLetters = DocumentCollection.GetCollection(docType).Documents;
            ObservableCollection<Letter> letters = new ObservableCollection<Letter>();
            OrganizationDictionary organizationDictionary = new OrganizationDictionary();
            ProjectDictionary projectDictionary = new ProjectDictionary();
            var projects = projectDictionary.GetAll();
            var organizations = organizationDictionary.GetAll();
            foreach (SaveDocument incomingLetter in saveLetters)
            {
                Project currentLetterProject = projects.FirstOrDefault(project => project.Id == incomingLetter.ProjectID);
                Organization currentLetterOrganization = organizations.FirstOrDefault(organization => organization.Id == incomingLetter.OrganizationID);
                Letter letter = new Letter(incomingLetter);
                letter.ProjectName = currentLetterProject.Name;
                letter.OrganizationName = currentLetterOrganization.Name;
                letters.Add(letter);
            }
            return letters;
        }
    }
}
