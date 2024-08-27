using BaseLibrary.Entities;
using Blazored.LocalStorage;
using ClientBlazorWebApp.ApplicationStates;
using ClientBlazorWebApp.Components;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using ClientLibrary.Services.Implementations;
using Microsoft.AspNetCore.Components.Authorization;

namespace ClientBlazorWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddTransient<CustomHttpHandler>();
            builder.Services.AddHttpClient("SystemApiClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:7016/");
            });
            // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7016/") });
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<GetHttpClient>();
            builder.Services.AddScoped<LocalStorageService>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<AllState>();
            builder.Services.AddScoped<IUserAccountService, UserAccountService>();
            //General Department/Department/Branch
            builder.Services.AddScoped<IGenericServiceInterface<GeneralDepartment>, GenericServiceImplementation<GeneralDepartment>>();
            builder.Services.AddScoped<IGenericServiceInterface<Department>, GenericServiceImplementation<Department>>();
            builder.Services.AddScoped<IGenericServiceInterface<Branch>, GenericServiceImplementation<Branch>>();


            // Country/City/Town
            builder.Services.AddScoped<IGenericServiceInterface<Country>, GenericServiceImplementation<Country>>();
            builder.Services.AddScoped<IGenericServiceInterface<City>, GenericServiceImplementation<City>>();
            builder.Services.AddScoped<IGenericServiceInterface<Town>, GenericServiceImplementation<Town>>();
            //employee
            builder.Services.AddScoped<IGenericServiceInterface<Employee>, GenericServiceImplementation<Employee>>();

            //Overtime, Sanction....

            builder.Services.AddScoped<IGenericServiceInterface<Vacation>, GenericServiceImplementation<Vacation>>();
            builder.Services.AddScoped<IGenericServiceInterface<VacationType>, GenericServiceImplementation<VacationType>>();


            builder.Services.AddScoped<IGenericServiceInterface<Overtime>, GenericServiceImplementation<Overtime>>();
            builder.Services.AddScoped<IGenericServiceInterface<OvertimeType>, GenericServiceImplementation<OvertimeType>>();

            builder.Services.AddScoped<IGenericServiceInterface<Sanction>, GenericServiceImplementation<Sanction>>();
            builder.Services.AddScoped<IGenericServiceInterface<SanctionType>, GenericServiceImplementation<SanctionType>>();

            builder.Services.AddScoped<IGenericServiceInterface<Doctor>, GenericServiceImplementation<Doctor>>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
