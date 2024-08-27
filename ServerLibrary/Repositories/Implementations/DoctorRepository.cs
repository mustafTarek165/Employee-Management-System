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
    public class DoctorRepository : GenericRepository<Doctor>, IDoctorRepository
    {
        private AppDbContext _appDbContext;
        public DoctorRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        //all custom logic and ovverides here

        /*

        
        */
        public async Task<GeneralResponse> Update(Doctor item1)
        {
            var item = await _appDbContext.Doctors.FirstOrDefaultAsync(x => x.EmployeeId == item1.EmployeeId);
            if (item is null) return new(false,"Item Not Found");

            item.MedicalRecommendation = item1.MedicalRecommendation;
            item.MedicalDiagnose = item1.MedicalDiagnose;
            item.Date = item1.Date;
            await _appDbContext.SaveChangesAsync();
            return new (true, "Item Updated Successfully");
        }

    }
}

