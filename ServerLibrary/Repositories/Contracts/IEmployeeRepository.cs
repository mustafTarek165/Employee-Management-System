using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories.Contracts
{
    public interface IEmployeeRepository : IGenericRepositoryInterface<Employee>
    {
        // Other custom methods can be defined here
    }
}
