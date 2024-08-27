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
    public class OvertimeRepository : GenericRepository<Overtime>, IOvertimeRepository
    {
        private AppDbContext _appDbContext;
        public OvertimeRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        //all custom logic and ovverides here

        /*

        
        */
        public async Task<GeneralResponse> Update(Overtime Overtime)
        {
            var dep = await _appDbContext.Overtimes.FindAsync(Overtime.Id);
            if (dep is null) return new(false, "Item Not Found");

            dep.StartDate = Overtime.StartDate;
            dep.EndDate = Overtime.EndDate;
            dep.OvertimeTypeId = Overtime.OvertimeTypeId;

            await _appDbContext.SaveChangesAsync();
            return new(true, "Item Updated Successfully");
        }
        public override async Task<List<Overtime>> GetAll()
        => await _appDbContext.Overtimes.AsNoTracking().Include(x => x.OvertimeType).ToListAsync();



    }
}
