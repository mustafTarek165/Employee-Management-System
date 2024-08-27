using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VacationController : GenericController<Vacation>
    {
        public VacationController(IVacationRepository repository) : base(repository)
        {
        }

        // Add additional actions specific to CountryController if needed
    }
}
