using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace Store
{
    public class StoreDB
    {
        private string _connectionString;
        private SqliteConnection _connection;
        private SqliteCommand _query;

        public StoreDB(string patch)
        {
            _connectionString = $"Data Source={patch};Mode=ReadWrite;";
            try
            {
                _connection = new SqliteConnection(_connectionString);
                _query = new SqliteCommand { Connection = _connection };
            }
            catch (Exception)
            {
                throw new Exception("Путь к базе данных не найден");
            }
        }

        public void Open()
        {
            try
            {
                _connection.Open();
               // Console.WriteLine("Успешное подключение к базе данных");
            }
            catch (InvalidOperationException)
            {
                throw new Exception("Ошибка открытия базы данных");
            }
            catch (SqliteException)
            {
                throw new Exception("Подключаемся к уже открытой базе данных");
            }
            catch (Exception)
            {
                throw new Exception("Путь к базе данных не найден");
            }
        }

        public void Close()
        {
            _connection.Close();
        }

        public void ShowAllGoods()
        {
            Open();
            _query.CommandText = "SELECT * FROM table_goods;";
            var result = _query.ExecuteReader();

            if (!result.HasRows)
            {
                Console.WriteLine("Нет данных");
                return;
            }

            var goodsList = new List<Goods>();
            do
            {
                while (result.Read())
                {
                    /*
                    var Id = result.GetInt32(3);
                    _query.CommandText = $"SELECT type_name FROM table_type_goods WHERE type_id={Id};";
                    var resultType = _query.ExecuteReader();
                    resultType.Read();     
                    */
                    var goods = new Goods
                    {
                        IdGoods = result.GetInt32(0),
                        NameGoods = result.GetString(1),
                        Unit = result.GetString(2),
                        IdType = result.GetInt32(3)
                        //IdType = resultType.GetString(0)
                    };
                    
                    goodsList.Add(goods);
                }
            } while (result.NextResult());

            Console.WriteLine(" --------------------------------------------");
            Console.WriteLine(" ID |  Название  | Ед.измер. |  Тип товара  ");
            Console.WriteLine(" --------------------------------------------");
            foreach (var goods in goodsList)
                Console.WriteLine($" {goods.IdGoods} | {goods.NameGoods} | {goods.Unit} | {goods.IdType} ");
            Console.WriteLine(" --------------------------------------------");

            if (result != null) result.Close();
            Close();
        }

        public List<Suppliers> AllSuppliers()
        {
            Open();
            _query.CommandText = "SELECT * FROM table_supplier;";
            var result = _query.ExecuteReader();

            if (!result.HasRows)
            {
                Console.WriteLine("Нет данных");
                return null;
            }

            var suppliers = new List<Suppliers>();
            do
            {
                while (result.Read())
                {                    
                    var supplier = new Suppliers
                    {
                        IdSupplier = result.GetInt32(0),
                        NameSupplier = result.GetString(1),
                        Phone = result.GetString(2)
                    };

                    suppliers.Add(supplier);
                }
            } while (result.NextResult());
                       
            if (result != null) result.Close();
            Close();
            return suppliers;
        }

        public void ShowAllSuppliers()
        {            
            var suppliers = AllSuppliers();

            Console.WriteLine(" --------------------------------------------");
            Console.WriteLine(" ID |  Название поставщика  | Телефон ");
            Console.WriteLine(" --------------------------------------------");
            foreach (var supplier in suppliers)
                Console.WriteLine($" {supplier.IdSupplier} | {supplier.NameSupplier} | {supplier.Phone}");
            Console.WriteLine(" --------------------------------------------");
        }

        public List<Consignments> AllConsignments()
        {
            Open();
            _query.CommandText = "SELECT * FROM table_consignments;";
            var result = _query.ExecuteReader();

            if (!result.HasRows)
            {
                Console.WriteLine("Нет данных");
                return null;
            }

            var consignments = new List<Consignments>();
            do
            {
                while (result.Read())
                {
                    var consignment = new Consignments
                    {
                        IdConsignment = result.GetInt32(0),
                        Date = result.GetString(1),
                        IdGoods = result.GetInt32(2),
                        IdSupplier = result.GetInt32(3),
                        Amount = result.GetInt32(4),
                        SupplierPrice = result.GetInt32(5),
                    };
                    consignments.Add(consignment);
                }
            } while (result.NextResult());                      

            if (result != null) result.Close();
            Close();
            return consignments;
        }

        public void ShowAllConsignments()
        {
            var consignments = AllConsignments();

            Console.WriteLine(" --------------------------------------------");
            Console.WriteLine(" ID |  Дата поставки  |   Товар   |  Поставщик  | Количество | Цена поставщика ");
            Console.WriteLine(" --------------------------------------------");
            foreach (var consignment in consignments)
                Console.WriteLine($" {consignment.IdConsignment} | {consignment.Date} | {consignment.IdGoods} " +
                    $"| {consignment.SupplierPrice} | {consignment.Amount} | {consignment.SupplierPrice}");
            Console.WriteLine(" --------------------------------------------");
        }

        public void AddGoods(string nameGoods, string unit, int idType)
        {            
            Open();
            SqliteParameter nameParam = new SqliteParameter("@name", nameGoods);
            _query.Parameters.Add(nameParam);
            SqliteParameter unitParam = new SqliteParameter("@unit", unit);
            _query.Parameters.Add(unitParam);
            SqliteParameter idTypeParam = new SqliteParameter("@idType", idType);
            _query.Parameters.Add(idTypeParam);
            _query.CommandText = "INSERT INTO table_goods (goods_name, unit, type_id) VALUES (@name, @unit, @idType);";            
            _query.ExecuteNonQuery();            
            Close();
            ShowAllGoods();
        }

        public void DeleteGoods(string nameGoods)
        {            
            Open();                        
            _query.CommandText = $"DELETE FROM table_goods WHERE goods_name='{nameGoods}';";
            _query.ExecuteNonQuery();
            Close();
            ShowAllGoods();
        }

        public void UpdateSupplier(string nameSupplier, string phone)
        {
            Open();
            _query.CommandText = $"UPDATE table_supplier SET phone='{phone}' WHERE supplier_name='{nameSupplier}';";
            _query.ExecuteNonQuery();
            Close();
            ShowAllSuppliers();
        }

        public void GoodsNumberSupplier()
        {
            Console.WriteLine("Количество товаров на складе по поставщикам");
            var consignments = AllConsignments();
            var suppliers = AllSuppliers();

            foreach(var supplier in suppliers)
            {
                var count = 0;
                foreach (var consignment in consignments)
                        if (supplier.IdSupplier == consignment.IdSupplier) count += consignment.Amount;
                Console.WriteLine($"{supplier.NameSupplier} - {count}");
            }
        }

        public void MaxGoodsNumberSupplier()
        {
            var max = 0;
            var maxSupplier = "";
            var consignments = AllConsignments();
            var suppliers = AllSuppliers();

            foreach (var supplier in suppliers)
            {
                var count = 0;
                foreach (var consignment in consignments)
                        if (supplier.IdSupplier == consignment.IdSupplier) count += consignment.Amount;

                if (max < count)
                {
                    max = count;
                    maxSupplier = supplier.NameSupplier;
                }                
            }

            foreach (var supplier in suppliers)
                   if(supplier.NameSupplier== maxSupplier) Console.WriteLine($"Больше всего товаров на складе от поставщика " +
                       $"{supplier.NameSupplier}, телефон - {supplier.Phone}, в количестве {max}");           
        }

        public void DeliveryTimeGoods(int number)
        {
            Console.WriteLine($"Товары с поставки, которых прошло {number} дней");
            var todayDate = DateTime.Now;
            var consignments = AllConsignments();
            
            foreach (var consignment in consignments)
            {
                string dateyear = "";
                string datemonth = "";
                string dateday = "";

                char[] dateCon = consignment.Date.ToCharArray();
                for (int i = 0; i < dateCon.Length; i++)
                {
                    if (i < 4) dateyear += dateCon[i];
                    if (i > 3 && i < 6) datemonth += dateCon[i];
                    if (i > 5 && i < 8) dateday += dateCon[i];
                }

                var year = Convert.ToInt32(dateyear);
                var month = Convert.ToInt32(datemonth);
                var day = Convert.ToInt32(dateday);

                DateTime date1 = new DateTime(year, month, day);                
                var date2= todayDate.AddDays(-number);
                if (date1 <= date2) Console.WriteLine(date1.ToShortDateString());
            }            
        }
    }
}
