using Estacionat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionat.Services
{
    public class SellerService
    {
        private readonly EstacionatContext _context;

        public SellerService(EstacionatContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {

            return _context.Seller.ToList();

        }

        internal void Insert(Seller seller)
        {
            seller.Department = _context.Department.FirstOrDefault();

            _context.Seller.Add(seller);
            _context.SaveChanges();
        }
    }
}
