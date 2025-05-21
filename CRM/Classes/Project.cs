using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Classes
{
    internal class Project
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string ProjectNumber {  get; private set; }
        public string DisplayValue => $"{ProjectNumber} {Name}";
        public Project(string name, string projectNumber, int id)
        {
            Id = id;
            Name = name;
            ProjectNumber = projectNumber;
        }
    }
}
