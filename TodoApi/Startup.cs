﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;//database
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApi.Models; //self 

namespace TodoApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(
                opt => opt.UseInMemoryDatabase("TodoList"));
            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    
        public Startup(IConfiguration configuration)
        {
            IConfiguration Configuration = configuration;
            var s1 = ConfigurationExtensions.GetConnectionString(Configuration, "DefaultConnection");
            var s2 = ConfigurationExtensions.GetConnectionString(Configuration, "Connection");
            var s4 = ConfigurationExtensions.GetConnectionString(Configuration, "cc");
            var s5 = ConfigurationExtensions.GetConnectionString(Configuration, "aa:cc");
            var s = $"Test==>{s5}";

        

        }    
    }
}