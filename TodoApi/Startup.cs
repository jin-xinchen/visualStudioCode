using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;//database
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Models; //self 
using CorrelationId;

using Swashbuckle.AspNetCore.Swagger;
namespace TodoApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(
                opt => opt.UseInMemoryDatabase("TodoList"));

            services.AddDbContext<TodoContext>(options => options.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCorrelationId();
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseCorrelationId("testRelationID");

            app.UseMvc();
        }
        IConfiguration Configuration;
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
            var s1 = ConfigurationExtensions.GetConnectionString(Configuration, "DefaultConnection");
            var s2 = ConfigurationExtensions.GetConnectionString(Configuration, "Connection");
            var s4 = ConfigurationExtensions.GetConnectionString(Configuration, "cc");
            var s5 = ConfigurationExtensions.GetConnectionString(Configuration, "aa:cc");
            var s = $"Test==>{s5}";
            var s6 = Configuration.GetConnectionString("aa:cc");
            var s7 = Configuration.GetConnectionString("DefaultConnection1");
            var s8 = Configuration.GetConnectionString("Log_path");
            var s9 = Configuration["Log_path"];



        }
    }
}