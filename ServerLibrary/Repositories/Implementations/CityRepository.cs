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
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        private AppDbContext _appDbContext;
        public CityRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        //all custom logic and ovverides here

        /*

        
        */
        public async Task<GeneralResponse> Update(City city)
        {
            var dep = await _appDbContext.Cities.FindAsync(city.Id);
            if (dep is null) return new(false, "Item Not Found");

            dep.Name = city.Name;
            dep.CountryId = city.CountryId;
            await _appDbContext.SaveChangesAsync();
            return new(true, "Item Updated Successfully");
        }
        public override async Task<List<City>> GetAll()
        => await _appDbContext.Cities.AsNoTracking().Include(x => x.Country).ToListAsync();


    }

}

