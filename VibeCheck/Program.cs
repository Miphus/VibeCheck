using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VibeCheck.Models;
using VibeCheck.Services;

namespace VibeCheck
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddTransient<DataService>();
            builder.Services.AddTransient<StateService>();
            builder.Services.AddTransient<AccountService>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSession();

            var connString = builder.Configuration.GetConnectionString("DefaultConnection");

            // Registrera Context-klassen för dependency injection
            builder.Services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(connString));

            // 1. Registera identity-klasserna och vilken DbContext som ska användas
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Här kan vi (om vi vill) ange inställningar för t.ex. lösenord
                // (ofta struntar man i detta och kör på default-värdena)
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
            })
                .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            // 2. Specificera att auth cookies ska användas och URL till inloggnings-sidan
            builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/login");

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();




            var app = builder.Build();

            var cultureInfo = new CultureInfo("en-US");

            // Formatering av nummer och datum
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;

            // Vilket språk som ska användas
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseHttpsRedirection();

            // 3. Kontrollera auth-cookien
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(); // Nås på URL “/swagger”

            // Applikationen måste instrueras att använda session
            app.UseSession();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/error/exception");
                app.UseStatusCodePagesWithRedirects("/error/http/{0}");
            }

            // Stöd för Route-attribut på våra Action-metoder
            app.MapControllers();

            // Stöd för statiska filer
            app.UseStaticFiles();

            app.Run();
        }
    }
}
