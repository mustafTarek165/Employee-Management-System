using BaseLibrary.DTOs;
using BaseLibrary.Entities.IdentityEntities;
using BaseLibrary.Responses;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServerLibrary.Constants;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using BaseLibrary.Entities;
using System.Reflection.Metadata.Ecma335;
namespace ServerLibrary.Repositories.Implementations
{
    public class UserAccountRepository(IOptions<JwtSection>config,AppDbContext _dbContext) : IUserAccount
    {
        public async Task<GeneralResponse> CreateAsync(Register user)
        {
            if (user == null)  return new GeneralResponse(false, "Model is empty");
           //check and add user
                var check = await FindUserByEmail(user.Email!);
                if (check != null) return new GeneralResponse(false, "User is already Registered");

                var applicationUser = await AddToDatabase(new ApplicationUser
                {
                    Name = user.FullName,
                    Email = user.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(user.Password)
                });
            //check and add admin role
            var CheckAdminRole =await _dbContext.SystemRoles.FirstOrDefaultAsync(x=>x.Name!.Equals(Constants.Constants.Admin));
            if(CheckAdminRole is null)
            {
                var CreateAdminRole= await AddToDatabase(new SystemRole { Name = Constants.Constants.Admin});
                await AddToDatabase(new UserRole { RoleId = CreateAdminRole.Id, UserId = applicationUser.Id });
                return new GeneralResponse(true, "Account Created Successfully!");
            }
            //check and add user role
            var CheckUserRole =await _dbContext.SystemRoles.FirstOrDefaultAsync(x => x.Name!.Equals(Constants.Constants.User));
            SystemRole response = new();
            if (CheckUserRole is null)
            {
                response = await AddToDatabase(new SystemRole { Name = Constants.Constants.User });
                await AddToDatabase(new UserRole { RoleId = response.Id, UserId = applicationUser.Id });
             
            }
            else
            {
                await AddToDatabase(new UserRole { RoleId = CheckUserRole.Id, UserId = applicationUser.Id });
            }
            return new GeneralResponse(true, "Account Created Successfully!");
        }

        public async Task<LoginResponse> SignInAsync(Login user)
        {
            if (user == null) return new LoginResponse(false, "Model is empty");

            //check user email and password
            var applicationUser = await FindUserByEmail(user.Email!);
            if (applicationUser is null) return new LoginResponse(false, "User not found");
            if (!BCrypt.Net.BCrypt.Verify(user.Password, applicationUser.Password))
                return new LoginResponse(false, "Email/Password not found");
            //check role
            var getUserRole = await FindUserRole(applicationUser.Id);
            if (getUserRole is null) return new LoginResponse(false, "User Role Not Found");

            //get Role name

            var getRoleName = await FindRoleName(getUserRole.RoleId);
            if (getRoleName is null) return new LoginResponse(false, "User Role Name Not Found");

            //generate token
            string jwtToken = GenerateToken(applicationUser, getRoleName!.Name!);
            string refreshToken = GenerateRefreshToken();
            //succesful login so save refresh token to database
            var FindUser = await _dbContext.RefreshTokenInfos.FirstOrDefaultAsync(x => x.UserId == applicationUser.Id);
            if(FindUser is not null)
            {
                FindUser.Token = refreshToken;
                await _dbContext.SaveChangesAsync();    
            }
            else
            {
                await AddToDatabase(new RefreshTokenInfo() { Token = refreshToken,UserId=applicationUser.Id });
            }

            return new LoginResponse(true, "Login Successfully",jwtToken,refreshToken);
        }
        private string GenerateToken(ApplicationUser user,string role)
        {
            var SecurityKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Value.Key!));
            var Cardentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var UserClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name!),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role,role!)
            };

            var token = new JwtSecurityToken(
                issuer:config.Value.Issuer,
                audience:config.Value.Audience,
                claims:UserClaims,
                expires:DateTime.Now.AddSeconds(2),
                signingCredentials:Cardentials
                
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
       

        public async Task<LoginResponse> RefreshTokenAsync(RefreshToken token)
        {
            if (token == null) return new LoginResponse(false, "Model is empty");

            var findToken=await _dbContext.RefreshTokenInfos.FirstOrDefaultAsync(x=>x.Token!.Equals(token.Token));
            if (findToken == null) return new LoginResponse(false, "Refresh Token is required");

            //get user details
            var user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == findToken.UserId);
            if (user is null) return new LoginResponse(false, "Refresh Token can't be generated as user doesn't exist");

            var userRole = await FindUserRole(user.Id);
            var roleName = await FindRoleName(userRole.RoleId);
            
            string jwtToken = GenerateToken(user, roleName.Name!);
            string refreshToken = GenerateRefreshToken();

            var UpdateRefreshToken=await _dbContext.RefreshTokenInfos.FirstOrDefaultAsync(x=>x.UserId==user.Id);
            if (UpdateRefreshToken is null) return new LoginResponse(false, "Refresh Token can't be generated as user not sign in");


            UpdateRefreshToken.Token = refreshToken;
            await _dbContext.SaveChangesAsync();
            return new LoginResponse(true, "Token refreshed Successfully", jwtToken, refreshToken);
        }
        private static string GenerateRefreshToken() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        private async Task<UserRole?> FindUserRole(int userId) => await _dbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == userId);
        private async Task<SystemRole?> FindRoleName(int roleId) => await _dbContext.SystemRoles.FirstOrDefaultAsync(x => x.Id == roleId);

        private Task<ApplicationUser?> FindUserByEmail(string email) => _dbContext.ApplicationUsers.
            FirstOrDefaultAsync(u => u.Email!.ToLower()!.Equals(email!.ToLower()));

        private async Task<T> AddToDatabase<T>(T model)
        {
            try
            {
                var result = _dbContext.AddAsync(model!);
                await _dbContext.SaveChangesAsync();
                return (T)result.Result.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
      
        public async Task<List<ManageUser>> GetUsers()
        {
           var allUsers=await GetApplicationUsers();
            var allUserRoles = await UserRoles();
            var allRoles = await SystemRoles();
            if (allUsers.Count == 0 || allRoles.Count == 0) return null!;
            var users = new List<ManageUser>(); 
            foreach (var user in allUsers) {
                var userRole = allUserRoles.FirstOrDefault(x => x.UserId == user.Id);
                var roleName = allRoles.FirstOrDefault(x => x.Id == userRole.RoleId);
                users.Add(new ManageUser() { UserId=user.Id,Name=user.Name,Email=user.Email,Role=roleName.Name});
           
            }
            return users;
        }

        public async Task<GeneralResponse> UpdateUser(ManageUser user)
        {
           
            var getRole = (await SystemRoles()).FirstOrDefault(x => x.Id==int.Parse(user.Role));
            var userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.UserId);
            userRole.RoleId = getRole.Id;
            await _dbContext.SaveChangesAsync();
            return new GeneralResponse(true, "User role updated Successfully");
        }

        public async Task<List<SystemRole>> GetRoles()
      => await SystemRoles();

        public async Task<GeneralResponse> DeleteUser(int id)
        {
            var user = await _dbContext.ApplicationUsers.FirstOrDefaultAsync(x => x.Id == id);
            _dbContext.ApplicationUsers.Remove(user);
           var userRole= await _dbContext.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            _dbContext.UserRoles.Remove(userRole);

            await _dbContext.SaveChangesAsync();
            return new GeneralResponse(true, "User successfully deleted");
        }
        private async Task<List<SystemRole>> SystemRoles()
          => await _dbContext.SystemRoles.AsNoTracking().ToListAsync();
        private async Task<List<UserRole>> UserRoles()
            => await _dbContext.UserRoles.AsNoTracking().ToListAsync();
        private async Task<List<ApplicationUser>> GetApplicationUsers()
                    => await _dbContext.ApplicationUsers.AsNoTracking().ToListAsync();

    }
}
