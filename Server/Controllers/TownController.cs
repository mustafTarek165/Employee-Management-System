using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TownController : GenericController<Town>
    {
        public TownController(ITownRepository repository) : base(repository)
        {
        }

        // Add additional actions specific to CountryController if needed
    }
}
