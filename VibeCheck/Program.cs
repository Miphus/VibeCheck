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

            // Registrera Context-klassen f�r dependency injection
            builder.Services.AddDbContext<ApplicationContext>(o => o.UseSqlServer(connString));

            // 1. Registera identity-klasserna och vilken DbContext som ska anv�ndas
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // H�r kan vi (om vi vill) ange inst�llningar f�r t.ex. l�senord
                // (ofta struntar man i detta och k�r p� default-v�rdena)
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = true;
            })
                .AddEntityFrameworkStores<ApplicationContext>().AddDefaultTokenProviders();

            // 2. Specificera att auth cookies ska anv�ndas och URL till inloggnings-sidan
            builder.Services.ConfigureApplicationCookie(o => o.LoginPath = "/login");

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();




            var app = builder.Build();

            var cultureInfo = new CultureInfo("en-US");

            // Formatering av nummer och datum
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;

            // Vilket spr�k som ska anv�ndas
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            app.UseHttpsRedirection();

            // 3. Kontrollera auth-cookien
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(); // N�s p� URL �/swagger�

            // Applikationen m�ste instrueras att anv�nda session
            app.UseSession();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/error/exception");
                app.UseStatusCodePagesWithRedirects("/error/http/{0}");
            }

            // St�d f�r Route-attribut p� v�ra Action-metoder
            app.MapControllers();

            // St�d f�r statiska filer
            app.UseStaticFiles();

            app.Run();
        }
    }
}
