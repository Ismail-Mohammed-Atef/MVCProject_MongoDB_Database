using Demo.BLL.Interfaces;
using Demo.DAL.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repostitories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MongoDbContext _context;

        private IDepartmentRepository departmentRepository;
        private IEmployeeRepository employeeRepository;

        public UnitOfWork(MongoDbContext context,string collectionName1 = "department" , string collectionName2 = "employee")
        {
            _context = context;
            departmentRepository = new DepartmentRepository(_context, collectionName1);
            employeeRepository = new EmployeeRepository(_context, collectionName2);
        }

        public IDepartmentRepository DepartmentRepository => departmentRepository;
        public IEmployeeRepository EmployeeRepository => employeeRepository;

        //public void Dispose()
        //{
        //    _context.Dispose();
        //}
        //public int Complete()
        //{
        //    return _context.SaveChanges();
        //}
    }
}
