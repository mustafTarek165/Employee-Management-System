using BaseLibrary.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController(IUserAccount UserAccount) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult>CreateAsync(Register user)
        {

            if (user == null) return BadRequest("Model is empty");
            var account=await UserAccount.CreateAsync(user);
            return Ok(account);
        }
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(Login user)
        {

            if (user == null) return BadRequest("Model is empty");
            var account = await UserAccount.SignInAsync(user);
            return Ok(account);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(RefreshToken token)
        {

            if (token == null) return BadRequest("Model is empty");
            var account = await UserAccount.RefreshTokenAsync(token);
            return Ok(account);
        }
        [HttpGet("users")]
        public async Task<IActionResult>GetUsersAsync()
        {
            var users = await UserAccount.GetUsers();
            if (users == null) return NotFound();
            return Ok(users);
        }
        [HttpPut("update-user")]
        public async Task<IActionResult> UpdateUser(ManageUser manageUser)
        {
            var result = await UserAccount.UpdateUser(manageUser);
            
            return Ok(result);
        }
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var users = await UserAccount.GetRoles();
            if (users == null) return NotFound();
            return Ok(users);
        }
        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await UserAccount.DeleteUser(id);

            return Ok(result);
        }
    }

}
