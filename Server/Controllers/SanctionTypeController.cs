using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanctionTypeController : GenericController<SanctionType>
    {
        public SanctionTypeController(ISanctionTypeRepository repository) : base(repository)
        {
        }

        // Add additional actions specific to SanctionTypeController if needed
    }
}
