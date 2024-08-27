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
    public class OvertimeTypeRepository : GenericRepository<OvertimeType>, IOvertimeTypeRepository
    {
        private AppDbContext _appDbContext;
        public OvertimeTypeRepository(AppDbContext context) : base(context)
        {
            _appDbContext = context;
        }
        public async Task<GeneralResponse> Update(OvertimeType OvertimeType)
        {
            var dep = await _appDbContext.OvertimeTypes.FindAsync(OvertimeType.Id);
            if (dep is null) return new(false, "Item Not Found");

            dep.Name = OvertimeType.Name;   
          
            await _appDbContext.SaveChangesAsync();
            return new(true, "Item Updated Successfully");
        }

    }
}
