using System.Collections.ObjectModel;

namespace CRM.Classes
{
    internal class OrganizationDictionary
    {
        private ObservableCollection<Organization> _projects;

        public OrganizationDictionary()
        {
            _projects = new ObservableCollection<Organization>()
            {
                new Organization("АО \"ОСК\"", 0),
                new Organization("АО ЦНКБ \"Алмаз\"",1),
                new Organization("ОАО ПО \"Севмаш\"", 2),
                new Organization("АО \"Балтийский завод\"", 3),
                new Organization("АО \"Кировэнергомаш\"", 4),
                new Organization("АО ПСЗ \"Янтарь\"", 5)
            };
        }
        
        public ObservableCollection<Organization> GetAll()
        {
            return _projects;
        }
    }
}

