using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Data;
using src.Interfaces;
using src.Models;

namespace src.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDBContext _context;
        public EmployeeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Employee> CreateAsync(Employee employeeModel)
        {
            await _context.Employees.AddAsync(employeeModel);
            await _context.SaveChangesAsync();

            return employeeModel;
        }
    }
}