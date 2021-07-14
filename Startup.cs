using Courses.Controllers;
using Courses.Models.Services.Application;
using Courses.Models.Services.Infrastructure;
using Courses.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Courses
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
          //services.AddTransient<ICourseService, AdoNetCourseService>();

           services.AddTransient<ICourseService, EfCoreCourseService>();

            services.AddTransient<IDatabaseAccessor, SqlDatabase>();

            services.AddTransient<ICachedCourseService, MemoryCacheService>();
            //Connected database
            services.AddDbContextPool<CoursesDbContext>(
                builder =>
                {
                    string connectionDb = Configuration.GetSection("ConnectionString").GetValue<string>("DbConnect");

                    builder.UseSqlServer(connectionDb);

                });

            //Options
            services.Configure<ConnectionStringOptions>(Configuration.GetSection("ConnectionString"));
            services.Configure<CoursesOptions>(Configuration.GetSection("Courses"));
            services.Configure<MemoryCacheOptions>(Configuration.GetSection("MemoryCache"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                string filePath = Path.Combine(env.ContentRootPath, "bin/reload.txt");
                File.WriteAllText(filePath, DateTime.Now.ToString());
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");

            });
        }
    }
}
