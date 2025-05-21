using CRM.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace CRM
{
    public partial class Window_Document : Window
    {
        public bool Resulte {  get; private set; }
        private Guid? _id;
        private string _pathFile;
        private DocumentType _docType;
        public Window_Document(Letter incomingLetter, DocumentType docType)
        {
            InitializeComponent();
            ProjectDictionary projects = new ProjectDictionary();
            OrganizationDictionary organizations = new OrganizationDictionary();
            CB_projects.ItemsSource = projects.GetAll();
            CB_organizations.ItemsSource = organizations.GetAll();
            _id = incomingLetter?.ID;
            _docType = docType;
            FillInfo(incomingLetter);
            switch (docType)
            {
                case DocumentType.Incoming:
                {
                    TB_Organization.Text = "Отправитель";
                    break;
                }
                case DocumentType.Outgoing:
                {
                    TB_Organization.Text = "Адресат";
                    break;
                }
                case DocumentType.KD:
                {
                    TB_Organization.Text = "Адресат";
                    break;
                }
            }
        }
        private void saveLetter(object sender, RoutedEventArgs e)
        {
            Project selectedProject = CB_projects.SelectedItem as Project;
            if (selectedProject == null ) 
            {
                return;
            }
            Organization selectedOrganization = CB_organizations.SelectedItem as Organization;
            if (selectedOrganization == null)
            {
                return;
            }
            Letter incomingLetter = new Letter(Subject.Text, Message.Text, selectedProject.Id, selectedOrganization.Id, _id, _pathFile, _docType);
            if (incomingLetter.CanSave() == true)
            {
                incomingLetter.Save();
                Resulte = true;
                Close();
            }
        }
        private void FillInfo(Letter incomingLetter)
        {
            if (incomingLetter != null)
            {
                Subject.Text = incomingLetter.Subject;
                Message.Text = incomingLetter.Message;
                CB_organizations.SelectedIndex = incomingLetter.OrganizationID;
                CB_projects.SelectedIndex = incomingLetter.ProjectID;
                _pathFile = incomingLetter.PathFile;
                DisplayFile(_pathFile);
                Title = $"Документ: {incomingLetter.Subject}";
            }
        }

        private void SelectFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.CheckFileExists = true;
            openFileDialog.AddExtension = true;
            openFileDialog.Multiselect = false;
            //openFileDialog.Filter = "PDF files (*.pdf)|*.pdf";

            if (openFileDialog.ShowDialog() == true)
            {
                _pathFile = Path.Combine("Files", Path.GetFileName(openFileDialog.FileName));
                File.Copy(openFileDialog.FileName, Path.Combine(Environment.CurrentDirectory, _pathFile), overwrite: true);

                DisplayFile(_pathFile);
            }
        }

        private void DisplayFile(string pathFile)
        {
            if (string.IsNullOrEmpty(pathFile))
            {
                return;
            }
            fileViewer.Navigate(new Uri(Path.Combine(Environment.CurrentDirectory, pathFile)));
        }
    }
}
