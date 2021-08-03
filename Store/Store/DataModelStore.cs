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
        public int IdSuppliers { get; set; }
        public string NameSuppliers { get; set; }
        public string Phone { get; set; }

        public Suppliers() { }
        public Suppliers(int idSuppliers, string nameSuppliers, string phone)
        {
            IdSuppliers = idSuppliers;
            NameSuppliers = nameSuppliers;
            Phone = phone;
        }
    }

    public class Consignments
    {
        public int IdConsignments { get; set; }
        public string Date { get; set; }
        public int IdGoods { get; set; }
        public int IdSuppliers { get; set; }
        public int Amount { get; set; }
        public int SupplierPrice { get; set; }

        public Consignments() { }
        public Consignments(int idConsignments, string date, int idGoods, int idSuppliers, int amount, int supplierPrice)
        {
            IdConsignments = idConsignments;
            Date = date;
            IdGoods = idGoods;
            IdSuppliers = idSuppliers;
            Amount = amount;
            SupplierPrice = supplierPrice;
        }
    }
}