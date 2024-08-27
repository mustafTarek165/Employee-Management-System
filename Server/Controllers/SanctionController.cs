using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SanctionController : GenericController<Sanction>
    {
        public SanctionController(ISanctionRepository repository) : base(repository)
        {
        }

        // Add additional actions specific to SanctionController if needed
    }
}
