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
    public class BranchRepository : GenericRepository<Branch>, IBranchRepository
    {
        private AppDbContext _appDbContext;
        public BranchRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        public async Task<GeneralResponse> Update(Branch branch)
        {
            var dep = await _appDbContext.Branchs.FindAsync(branch.Id);
            if (dep is null) return new(false, "Item Not Found");

            dep.Name = branch.Name;
            dep.DepartmentId = branch.DepartmentId;
            await _appDbContext.SaveChangesAsync();
            return new(true, "Item Updated Successfully");
        }
        public override async Task<List<Branch>> GetAll()
            => await _appDbContext.Branchs.AsNoTracking()
            .Include(x => x.Department).ToListAsync();



    }
}
