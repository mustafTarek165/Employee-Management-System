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
    public class CountryRepository : GenericRepository<Country>, ICountryRepository
    {
        private AppDbContext _appDbContext;
        public CountryRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        //all custom logic here

        /*

        
        */
        public async Task<GeneralResponse> Update(Country Country)
        {
            var dep = await _appDbContext.Countries.FindAsync(Country.Id);
            if (dep is null) return new(false, "Item Not Found");

            dep.Name = Country.Name;

            await _appDbContext.SaveChangesAsync();
            return new(true, "Item Updated Successfully");
        }
    }
}
