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
    public class VacationTypeRepository : GenericRepository<VacationType>, IVacationTypeRepository
    {
        private AppDbContext _appDbContext;
        public VacationTypeRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        //all custom logic and ovverides here

        /*

        
        */
        public async Task<GeneralResponse> Update(VacationType Vacation)
        {
            var dep = await _appDbContext.VacationTypes.FindAsync(Vacation.Id);
            if (dep is null) return new(false, "Item Not Found");

            dep.Name=Vacation.Name; 

            await _appDbContext.SaveChangesAsync();
            return new(true, "Item Updated Successfully");
        }

    }
}
