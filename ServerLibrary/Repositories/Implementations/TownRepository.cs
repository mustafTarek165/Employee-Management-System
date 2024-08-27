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
     public class TownRepository: GenericRepository<Town>, ITownRepository
    {
        private  AppDbContext _appDbContext;
        public TownRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;    
        }
        //all custom logic and ovverides here

        /*

        
        */
        public async Task<GeneralResponse> Update(Town town)
        {
            var dep = await _appDbContext.Towns.FindAsync(town.Id);
            if (dep is null) return new(false, "Item Not Found");

            dep.Name = town.Name;
            dep.CityId = town.CityId;
            await _appDbContext.SaveChangesAsync();
            return new(true, "Item Updated Successfully");
        }
        public override async Task<List<Town>> GetAll()
        => await _appDbContext.Towns.AsNoTracking().Include(x=>x.City).ToListAsync();  
        
         

     
      
    }
}
