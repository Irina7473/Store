using System;

namespace Store
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("  *** База данных склада ***");
            Console.WriteLine();
            var DB = new StoreDB(@"Store.sqlite");

            Console.WriteLine();
            DB.ShowAllGoods();
            Console.WriteLine();
            DB.ShowAllSuppliers();
            Console.WriteLine();
            DB.ShowAllConsignments();
            Console.WriteLine();

            
            DB.AddGoods("баклажаны", "кг", 1);
            Console.WriteLine();
        }
    }
}
