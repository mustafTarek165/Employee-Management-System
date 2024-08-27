using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OvertimeTypeController : GenericController<OvertimeType>
    {
        public OvertimeTypeController(IOvertimeTypeRepository repository) : base(repository)
        {
        }

        // Add additional actions specific to OvertimeTypeController if needed
    }
}
