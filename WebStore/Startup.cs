using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Infrastructure;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Services;
using WebStore.Infrastructure.Services.SQL;

namespace WebStore
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<WebStoreDB>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<WebStoreDBInitializer>();

            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            services.AddSingleton<IEmployeesData, InMemoryEmployeesData>();
            //services.AddSingleton<IProductData, InMemoryProductData>();
            services.AddScoped<IProductData, SqlProductData>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebStoreDBInitializer db)
        {
            db.Initialize();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();
            app.UseDefaultFiles();
            app.UseWelcomePage("/MVC");

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
               endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                    );
            });
        }
    }
}
