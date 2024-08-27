using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : GenericController<City>
    {

        public CityController(ICityRepository repository) : base(repository)
        {

        }


        // Add additional actions specific to CityController if needed
    }
}
