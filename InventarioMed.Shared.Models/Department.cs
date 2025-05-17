using InventarioMed_Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioMed.Shared.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Equipment> Equipment { get; set; }

        public override string ToString()
        {
            return $@"{Id}- Nome: {Name}";
        }
    }
}
