using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeController : GenericController<Overtime>
    {
        public OvertimeController(IOvertimeRepository repository) : base(repository)
        {
        }

        // Add additional actions specific to OvertimeController if needed
    }
}
