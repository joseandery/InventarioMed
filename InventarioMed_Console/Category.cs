using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioMed_Console
{
    internal class Category
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Category(string name)
        {
            Name = name;
            //Description = description;
        }
        public override string ToString()
        {
            return $@"Categoria do fabricante: {Name}";
        }
    }
}
