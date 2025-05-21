using System.Collections.ObjectModel;

namespace CRM.Classes
{
    internal class ProjectDictionary
    {
        private ObservableCollection<Project> _projects;

        public ProjectDictionary()
        {
            _projects = new ObservableCollection<Project>()
            {
                new Project("Щука-Б", "971", 0),
                new Project("Борей", "955", 1),
                new Project("Дельфин", "667БДРМ", 2),
                new Project("Стерегущий", "20380", 3),
                new Project("Гремящий", "20385", 4),
                new Project("Ясень", "885", 5),
                new Project("Адмирал Пантелеев", "1155", 6),
            };
        }

        public ObservableCollection<Project> GetAll()
        {
            return _projects;
        }
    }
}
