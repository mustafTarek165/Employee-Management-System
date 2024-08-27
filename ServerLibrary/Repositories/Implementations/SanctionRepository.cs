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
        public class SanctionRepository : GenericRepository<Sanction>, ISanctionRepository
        {
            private AppDbContext _appDbContext;
            public SanctionRepository(AppDbContext context) : base(context)
            {
                _appDbContext = context;
            }
            //all custom logic and ovverides here

            /*


            */
            public async Task<GeneralResponse> Update(Sanction Sanction)
            {
                var dep = await _appDbContext.Sanctions.FindAsync(Sanction.Id);
                if (dep is null) return new(false, "Item Not Found");

                dep.PunishmentDate = Sanction.PunishmentDate;
                dep.Punishment = Sanction.Punishment;
                dep.Date = Sanction.Date;

                dep.SanctionTypeId = Sanction.SanctionTypeId;

                await _appDbContext.SaveChangesAsync();
                return new(true, "Item Updated Successfully");
            }
            public override async Task<List<Sanction>> GetAll()
            => await _appDbContext.Sanctions.AsNoTracking().Include(x => x.SanctionType).ToListAsync();





        }
    }

