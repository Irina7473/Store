using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store
{
    public class Goods
    {
        public int IdGoods { get; set; }
        public string NameGoods { get; set; }
        public string Unit { get; set; }
        public int IdType { get; set; }
        
        public Goods() { }
        public Goods(int idGoods, string nameGoods, string unit, int idType)
        {
            IdGoods = idGoods;
            NameGoods = nameGoods;
            Unit = unit;
            IdType = idType;
        }
    }

    public class TypeGoods
    {
        public int IdType { get; set; }
        public string NameType { get; set; }
        
        public TypeGoods() { }
        public TypeGoods(int idType, string nameType)
        {
            IdType = idType;
            NameType = nameType;
        }
    }

    public class Suppliers
    {
        public int IdSupplier { get; set; }
        public string NameSupplier { get; set; }
        public string Phone { get; set; }

        public Suppliers() { }
        public Suppliers(int idSupplier, string nameSupplier, string phone)
        {
            IdSupplier = idSupplier;
            NameSupplier = nameSupplier;
            Phone = phone;
        }
    }

    public class Consignments
    {
        public int IdConsignment { get; set; }
        public string Date { get; set; }
        public int IdGoods { get; set; }
        public int IdSupplier { get; set; }
        public int Amount { get; set; }
        public int SupplierPrice { get; set; }

        public Consignments() { }
        public Consignments(int idConsignment, string date, int idGoods, int idSupplier, int amount, int supplierPrice)
        {
            IdConsignment = idConsignment;
            Date = date;
            IdGoods = idGoods;
            IdSupplier = idSupplier;
            Amount = amount;
            SupplierPrice = supplierPrice;
        }
    }
}