using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Classes
{
    internal class Organization
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public Organization(string name, int id)
        {
            Name = name;
            Id = id;
        }
    }
}
