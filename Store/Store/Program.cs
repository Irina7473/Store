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
            DB.DeleteGoods("арбуз");
            Console.WriteLine();
            DB.UpdateSupplier("Раздолье","4445556");
            Console.WriteLine();
            DB.GoodsNumberSupplier();
            Console.WriteLine();
            DB.MaxGoodsNumberSupplier();
            Console.WriteLine();
            DB.DeliveryTimeGoods(460);
            Console.WriteLine();
        }
    }
}
