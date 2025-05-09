using InventarioMed_Console;

internal class Program
{   
    public static Dictionary<string, Equipment> EquipmentList = new();
    private static void Main(string[] args)
    {
        

        bool exit = false;
        while (!exit)
        {
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
    }

    private static void CategoryGet()
    {
        Console.Clear();
        Console.WriteLine("Exibir detalhes do equipamento");
        Console.WriteLine("Digite o equipamento cujas categorias deseja consultar");
        string equipmentName = Console.ReadLine();
        if (EquipmentList.ContainsKey(equipmentName))
        {
            Equipment e = EquipmentList[equipmentName];
            e.ShowCategories();
        }
        else
        {
            Console.WriteLine($"O equipamento {equipmentName} não existe");
        }
    }

    private static void EquipmentGet()
    {
        Console.Clear();
        Console.WriteLine("Lista de equipamentos:");
        foreach (var item in EquipmentList.Values)
        {
            Console.WriteLine(item);
        }
    }

    private static void CategoryRegistration()
    {
        Console.Clear();
        Console.WriteLine("Registro de categorias");
        Console.WriteLine("Digite o nome do equipamento cuja categoria você deseja registrar");
        string equipmentName = Console.ReadLine();
        if (EquipmentList.ContainsKey(equipmentName))
        {
            Console.WriteLine($"Informe o nome da categoria do {equipmentName}");
            string name = Console.ReadLine();
            Equipment e = EquipmentList[equipmentName];
            e.AddCategory(new Category(name));
            Console.WriteLine($"A categoria {name} do {equipmentName} foi registrada com sucesso");
        }
        else
        {
            Console.WriteLine($"O equipamento {equipmentName} não existe");
        }
    }

    private static void EquipmentRegistration()
    {
        Console.Clear();
        Console.WriteLine("Registro de equipamento");
        Console.WriteLine("Digite o nome do equipamento que você deseja cadastrar");
        string name = Console.ReadLine();
        Console.WriteLine("Digite o fabricante do equipamento que você deseja cadastrar");
        string manufacturer = Console.ReadLine();
        Equipment e = new(name, manufacturer);
        EquipmentList.Add(name, e);
        Console.WriteLine($"Equipamento {name} adcionado com sucesso!");
    }
}