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
                Console.WriteLine("Успешное подключение к базе данных");
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

        public void ShowAllSuppliers()
        {
            Open();
            _query.CommandText = "SELECT * FROM table_supplier;";
            var result = _query.ExecuteReader();

            if (!result.HasRows)
            {
                Console.WriteLine("Нет данных");
                return;
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

            Console.WriteLine(" --------------------------------------------");
            Console.WriteLine(" ID |  Название поставщика  | Телефон ");
            Console.WriteLine(" --------------------------------------------");
            foreach (var supplier in suppliers)
                Console.WriteLine($" {supplier.IdSupplier} | {supplier.NameSupplier} | {supplier.Phone}");
            Console.WriteLine(" --------------------------------------------");

            if (result != null) result.Close();
            Close();
        }

        public void ShowAllConsignments()
        {
            Open();
            _query.CommandText = "SELECT * FROM table_consignments;";
            var result = _query.ExecuteReader();

            if (!result.HasRows)
            {
                Console.WriteLine("Нет данных");
                return;
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

            Console.WriteLine(" --------------------------------------------");
            Console.WriteLine(" ID |  Дата поставки  |   Товар   |  Поставщик  | Количество | Цена поставщика ");
            Console.WriteLine(" --------------------------------------------");
            foreach (var consignment in consignments)
                Console.WriteLine($" {consignment.IdConsignment} | {consignment.Date} | {consignment.IdGoods} " +
                    $"| {consignment.SupplierPrice} | {consignment.Amount} | {consignment.SupplierPrice}");
            Console.WriteLine(" --------------------------------------------");

            if (result != null) result.Close();
            Close();
        }

        public void AddGoods(string nameGoods, string unit, int idType)
        {
            //string sqlExpression = "INSERT INTO Users (Name, Age) VALUES (@name, @age)";
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
    }
}
