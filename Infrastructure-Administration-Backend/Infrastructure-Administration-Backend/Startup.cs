using Infrastructure_Administration_Backend.Data;
using Infrastructure_Administration_Backend.DataModels;
using Infrastructure_Administration_Backend.Repository;
using Infrastructure_Administration_Backend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Infrastructure_Administration_Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IRepository, InfrastructureRepository>();
            services.AddScoped<IService, InfrastructureService>();
            services.AddDbContext<InfrastructureAdminitrationDBContext>(option => option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(opts =>
            {
                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = true;
                opts.Password.RequireUppercase = true;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequiredLength = 6;
            })
             .AddRoles<IdentityRole>()
             .AddEntityFrameworkStores<InfrastructureAdminitrationDBContext>();
            services.AddHealthChecks();
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddMvc();
            //services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "InfrastructureAdministration",
                    Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>c.SwaggerEndpoint("/swagger/v1/swagger.json", "InfrastructureAdministration v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors(c => c.AllowAnyOrigin()
                              .WithOrigins("http://localhost:44324")
                              .WithMethods("GET", "POST", "PUT", "DELETE")
                              .AllowCredentials()
                              .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
