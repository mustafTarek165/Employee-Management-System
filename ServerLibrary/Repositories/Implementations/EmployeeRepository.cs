using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private AppDbContext _appDbContext;
        public EmployeeRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        public override async Task<List<Employee>> GetAll()
        {
            var employees = await _appDbContext.Employees.AsNoTracking()
            .Include(a => a.Town)
            .ThenInclude(b => b.City)
            .ThenInclude(c => c.Country)
            .Include(x => x.Branch)
            .ThenInclude(y => y.Department)
            .ThenInclude(z => z.generalDepartment).ToListAsync();
            return employees;
        }


        public override async Task<Employee?> GetById(int id)
        {
            var employee = await _appDbContext.Employees.AsNoTracking()
                      .Include(a => a.Town)
                      .ThenInclude(b => b.City)
                      .ThenInclude(c => c.Country)
                      .Include(x => x.Branch)
                      .ThenInclude(y => y.Department)
                      .ThenInclude(z => z.generalDepartment)
                      .FirstOrDefaultAsync(x => x.Id == id);
            return employee;
        }

        public async Task<GeneralResponse> Update(Employee item)
        {
            var employee = await _appDbContext.Employees.FindAsync(item.Id);
            if (employee is null) return new(false, "Item Not Found");

            employee.Name = item.Name;
            employee.Other = item.Other;
            employee.Address = item.Address;
            employee.TelephoneNumber = item.TelephoneNumber;
            employee.BranchId = item.BranchId;
            employee.TownId = item.TownId;
            employee.CivilId = item.CivilId;
            employee.FileNumber = item.FileNumber;
            employee.JobName = item.JobName;
            employee.Photo = item.Photo;

            await _appDbContext.SaveChangesAsync();
            return new(true, "Item Updated Successfully");
        }

    }
}



