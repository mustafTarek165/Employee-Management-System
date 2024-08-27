using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController<T> : ControllerBase 
        where T:class
    {
        private readonly IGenericRepositoryInterface<T> genericRepositoryInterface;

        protected GenericController(IGenericRepositoryInterface<T> repository)
        {
            genericRepositoryInterface = repository;
        }

        [HttpGet("all")]
        public  async Task<IActionResult>GetAll()=>Ok(await  genericRepositoryInterface.GetAll());
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult>Delete(int id)
        {
            var response = await genericRepositoryInterface.DeleteById(id);
            if (response.Flag) return Ok(response);
            return BadRequest(response);
        }
        [HttpGet("single/{id}")]
        public  async Task<IActionResult> GetById(int id)
        {
            var item = await genericRepositoryInterface.GetById(id);
            if (item==null) return NotFound();
            return Ok(item);
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(T model)
        {
            var response = await genericRepositoryInterface.Insert(model);
               if(response.Flag) return Ok(response);
               return BadRequest(response);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(T model)
        {
            var response = await genericRepositoryInterface.Update(model);
            if(response.Flag) return Ok(response);  
            return BadRequest(response);
        }
    }
}
