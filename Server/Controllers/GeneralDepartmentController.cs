using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralDepartmentController : GenericController<GeneralDepartment>
    {
        public GeneralDepartmentController(IGeneralDepartmentRepository repository) : base(repository)
        {
        }

        // Add additional actions specific to GeneralDepartmentController if needed
    }
}
