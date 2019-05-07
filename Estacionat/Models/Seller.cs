using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionat.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal BaseSalary { get; set; }
        public ICollection<SalesRecord> Sales { get; set; } = new Collection<SalesRecord>();
        public Department Department{ get; set; }
        public int DepartmentId{ get; set; }

        public Seller()
        {

        }

        public Seller(int id, string name, string email, DateTime birthDate, decimal baseSalary,Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void addSeles(SalesRecord salesRecord)
        {
            Sales.Add(salesRecord);
        }

        public void removeSales(SalesRecord salesRecord)
        {
            Sales.Remove(salesRecord);
        }

        public decimal totalSales(DateTime initial , DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
