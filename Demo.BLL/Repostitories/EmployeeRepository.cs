using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repostitories
{
    public class EmployeeRepository :GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(MongoDbContext context , string collectionName = "employee") : base(context, collectionName)
        {
            
        }

        //public async Task<IEnumerable<Employee>> GetName(string SearchInput)
        //{
        //    return await _context.Employees.Where(e=>e.Name.ToLower().Contains(SearchInput.ToLower())).Include(e=>e.Department).ToListAsync();
        //}
    }
}
