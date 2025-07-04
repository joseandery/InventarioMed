﻿using InventarioMed_Console;
using InventarioMed.Shared.Data.BD;
using System.Xml;

internal class Program
{
    public static Dictionary<string, Equipment> EquipmentList = new();
    private static void Main(string[] args)
    {
        var EquipmentDAL = new DAL<Equipment>();
        
        bool exit = false;
        while (!exit)
        {
            Console.Clear();
            Console.WriteLine("Você chegou na InventarioMed!\n");
            Console.WriteLine("Digite 1 para registrar um equipamento");
            Console.WriteLine("Digite 2 para registrar a categoria de um equipamento");
            Console.WriteLine("Digite 3 para mostrar todos os equipamentos");
            Console.WriteLine("Digite 4 para mostrar as categorias de um equipamento");
            Console.WriteLine("Digite -1 para sair\n");

            Console.WriteLine("Escolha sua opção");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    EquipmentRegistration();
                    break;
                case 2:
                    CategoryRegistration();
                    break;
                case 3:
                    EquipmentGet();
                    break;
                case 4:
                    CategoryGet();
                    break;
                case -1:
                    Console.WriteLine("Até mais");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }
        }
        void EquipmentRegistration()
        {
            Console.Clear();
            Console.WriteLine("Registro de equipamento");
            Console.WriteLine("Digite o nome do equipamento que você deseja cadastrar");
            string name = Console.ReadLine();
            Console.WriteLine("Digite o fabricante do equipamento que você deseja cadastrar");
            string manufacturer = Console.ReadLine();
            Equipment e = new(name, manufacturer);
            EquipmentDAL.Create(e);
            Console.WriteLine($"Equipamento {name} adcionado com sucesso!");
            Console.ReadKey();
        }
        void EquipmentGet()
        {
            Console.Clear();
            Console.WriteLine("Lista de equipamentos:");
            foreach (var item in EquipmentDAL.Read())
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
        void CategoryGet()
        {
            Console.Clear();
            Console.WriteLine("Exibir detalhes do equipamento");
            Console.WriteLine("Digite o equipamento cujas categorias deseja consultar");
            string equipmentName = Console.ReadLine();
            var targetEquip = EquipmentDAL.ReadBy(x => x.Name.Equals(equipmentName));
            if (targetEquip is not null) targetEquip.ShowCategories();
            else
            {
                Console.WriteLine($"O equipamento {equipmentName} não existe");
            }
            Console.ReadKey();
        }
        void CategoryRegistration()
        {
            Console.Clear();
            Console.WriteLine("registro de categorias");
            Console.WriteLine("digite o nome do equipamento cuja categoria você deseja registrar");
            string equipmentname = Console.ReadLine();
            var targetEquip = EquipmentDAL.ReadBy(x=>x.Name.Equals(equipmentname));
            if (targetEquip is not null)
            {
                Console.WriteLine($"Informe o nome da categoria do {equipmentname}");
                string name = Console.ReadLine();
                targetEquip.AddCategory(new Category(name));
                EquipmentDAL.Update(targetEquip);
                Console.WriteLine($"A categoria {name} do {equipmentname} foi registrada com sucesso");
            }
            else
            {
                Console.WriteLine($"O equipamento {equipmentname} não existe");
            }
            Console.ReadKey();
        }
    }

}