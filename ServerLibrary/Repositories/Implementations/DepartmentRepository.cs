using BaseLibrary.Entities;
using BaseLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ServerLibrary.Data;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Implementations
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private AppDbContext _appDbContext;
        public DepartmentRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        //all custom logic and ovverides here

        /*

        
        */
        public async Task<GeneralResponse> Update(Department department)
        {
            var dep = await _appDbContext.Departments.FindAsync(department.Id);
            if (dep is null) return new(false, "Item Not Found");

            dep.Name = department.Name;
            dep.GeneralDepartmentId = department.GeneralDepartmentId;
            await _appDbContext.SaveChangesAsync();
            return new(true, "Item Updated Successfully");
        }
        public override async Task<List<Department>> GetAll()
        => await _appDbContext.Departments.AsNoTracking().Include(x => x.generalDepartment).ToListAsync();

    }
}
