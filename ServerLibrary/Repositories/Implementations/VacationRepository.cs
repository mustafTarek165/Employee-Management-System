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
        public class VacationRepository : GenericRepository<Vacation>, IVacationRepository
        {
            private AppDbContext _appDbContext;
            public VacationRepository(AppDbContext context) : base(context)
            {
                _appDbContext = context;
            }
            //all custom logic and ovverides here

            /*


            */
            public async Task<GeneralResponse> Update(Vacation Vacation)
            {
                var dep = await _appDbContext.Vacations.FindAsync(Vacation.Id);
                if (dep is null) return new(false, "Item Not Found");

                dep.StartDate = Vacation.StartDate;
                dep.NumberOfDays = Vacation.NumberOfDays;


                dep.VacationTypeId = Vacation.VacationTypeId;

                await _appDbContext.SaveChangesAsync();
                return new(true, "Item Updated Successfully");
            }
            public override async Task<List<Vacation>> GetAll()
            => await _appDbContext.Vacations.AsNoTracking().Include(x => x.VacationType).ToListAsync();



        }
    }

