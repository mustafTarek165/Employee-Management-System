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
    public class GeneralDepartmentRepository : GenericRepository<GeneralDepartment>,IGeneralDepartmentRepository
    {
        private AppDbContext _appDbContext;
        public GeneralDepartmentRepository(AppDbContext context) : base(context)
        {
            _appDbContext=context;
        }
        //all custom logic here

        /*

        
        */
        public async Task<GeneralResponse> Update(OvertimeType OvertimeType)
        {
            var dep = await _appDbContext.GeneralDepartments.FindAsync(OvertimeType.Id);
            if (dep is null) return new(false, "Item Not Found");

            dep.Name = OvertimeType.Name;

            await _appDbContext.SaveChangesAsync();
            return new(true, "Item Updated Successfully");
        }
    }

 }
