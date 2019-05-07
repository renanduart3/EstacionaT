using Estacionat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionat.Services
{
    public class DepartmentService
    {
        private readonly EstacionatContext _context;

        public DepartmentService(EstacionatContext context)
        {
            _context = context;
        }

        public List<Department> FindAll()
        {
            return _context.Department.OrderBy(x => x.Name).ToList();
        }
    }
}
