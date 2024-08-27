
using BaseLibrary.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ServerLibrary.Data;
using ServerLibrary.Helpers;
using ServerLibrary.Repositories.Contracts;
using ServerLibrary.Repositories.Implementations;

using System.Text;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.Configure<JwtSection>(builder.Configuration.GetSection("JwtSection"));
            var jwtSection = builder.Configuration.GetSection(nameof(JwtSection)).Get<JwtSection>();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("Server"));
            });
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = jwtSection!.Issuer,
                    ValidAudience = jwtSection.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection.Key!))
                };
            });
            builder.Services.AddScoped(typeof(IGenericRepositoryInterface<>), typeof(GenericRepository<>));
           builder.Services.AddScoped<IUserAccount, UserAccountRepository>();

         builder.Services.AddScoped<IGeneralDepartmentRepository, GeneralDepartmentRepository>();
          builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IBranchRepository, BranchRepository>();

            builder.Services.AddScoped<ICountryRepository, CountryRepository>();
            builder.Services.AddScoped<ICityRepository, CityRepository>();
            builder.Services.AddScoped<ITownRepository, TownRepository>();

            builder.Services.AddScoped<IOvertimeRepository, OvertimeRepository>();
            builder.Services.AddScoped<IOvertimeTypeRepository, OvertimeTypeRepository>();

            builder.Services.AddScoped<ISanctionRepository, SanctionRepository>();
            builder.Services.AddScoped<ISanctionTypeRepository, SanctionTypeRepository>();

            builder.Services.AddScoped<IVacationRepository, VacationRepository>();
            builder.Services.AddScoped<IVacationTypeRepository, VacationTypeRepository>();

            builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();


            ////////////////









            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
        
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
