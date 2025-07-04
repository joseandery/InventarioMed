﻿using InventarioMed.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventarioMed_Console
{
    public class Equipment
    {
        public Equipment(string name, string manufacturer)
        {
            Name = name;
            Manufacturer = manufacturer;
        }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public int Id { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        public virtual ICollection<Department> Departments { get; set; }

        public override string ToString()
        {
            return $@"{Id}- Nome: {Name}";
        }
        public void AddCategory(Category c)
        {
            Categories.Add(c);
        }
        public void ShowCategories()
        {
            Console.WriteLine($"Categorias do equipamento {Name}");
            if (Categories.Count > 0)
            {
                foreach (var c in Categories)
                {
                    Console.WriteLine(c);
                }
            }
            else
            {
                Console.WriteLine("Nenhuma categoria registrada");
            }
        }
    }
}
