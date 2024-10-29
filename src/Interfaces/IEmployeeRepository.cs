using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using src.Models;

namespace src.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> CreateAsync(Employee employeeModel);
    }
}