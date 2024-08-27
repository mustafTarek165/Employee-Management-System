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
        public class SanctionTypeRepository : GenericRepository<SanctionType>, ISanctionTypeRepository
        {
            private AppDbContext _appDbContext;
            public SanctionTypeRepository(AppDbContext context) : base(context)
            {
                _appDbContext = context;
            }
            //all custom logic and ovverides here

            /*


            */
            public async Task<GeneralResponse> Update(SanctionType SanctionType)
            {
                var dep = await _appDbContext.SanctionTypes.FindAsync(SanctionType.Id);
                if (dep is null) return new(false, "Item Not Found");

                dep.Name = SanctionType.Name;
                await _appDbContext.SaveChangesAsync();
                return new(true, "Item Updated Successfully");
            }


        }
    }

