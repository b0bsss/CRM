using CRM.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CRM
{
    public partial class Page_Documents : Page
    {
        private ObservableCollection<Letter> _fullDocuments;
        private DocumentType _documentType;
        public Page_Documents(DocumentType docType)
        {
            InitializeComponent();

            _documentType = docType;
            LoadLetters();
            switch(docType)
            {
                case DocumentType.Incoming:
                {
                    DG_Documents.Columns[0].Header = "Отправитель";
                    BT_CreateDocument.Content = "Зарегестрировать входящее письмо";
                    break;
                }
                case DocumentType.Outgoing:
                {
                    DG_Documents.Columns[0].Header = "Адресат";
                    BT_CreateDocument.Content = "Написать исходящее письмо";
                    break;
                }
                case DocumentType.KD:
                {
                    DG_Documents.Columns[0].Header = "Адресат";
                    BT_CreateDocument.Content = "Зарегистрировать КД";
                    break;
                }
            }
        }

        private void AddDocument(object sender, RoutedEventArgs e)
        {
            Window_Document letterWindow = new Window_Document(null, _documentType);
            letterWindow.Show();
            letterWindow.Closing += OnWindowClosing;
        }


        public void OnWindowClosing(object sender, CancelEventArgs e)
        {
            var letterWindow = sender as Window_Document;
            letterWindow.Closing -= OnWindowClosing;
            if (letterWindow.Resulte == true)
            {
                LoadLetters();
            }
        }

        private void LoadLetters()
        {
            _fullDocuments = Letter.GetAll(_documentType);
            UpdateDisplayDocuments(_fullDocuments);
        }

        private void DG_Documents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Window_Document letterWindow = new Window_Document(DG_Documents.SelectedItem as Letter, _documentType);
            letterWindow.Show();
            letterWindow.Closing += OnWindowClosing;
        }

        private void UpdateDisplayDocuments(ObservableCollection<Letter> displayDocuments)
        {
            DG_Documents.ItemsSource = displayDocuments;
        }

        private void tb_searchDocuments_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string searchText = tb_searchDocuments.Text.ToLower();

            var displayDocuments = new ObservableCollection<Letter>(_fullDocuments.Where(doc => doc.OrganizationName.ToLower().Contains(searchText) ||
            doc.ProjectName.ToLower().Contains(searchText) || doc.Subject.ToLower().Contains(searchText) || doc.Message.ToLower().Contains(searchText)));

            UpdateDisplayDocuments(displayDocuments);
        }
    }
}
