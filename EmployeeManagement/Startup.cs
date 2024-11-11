using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmployeeManagement
{
    public class Startup
    {
        private IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 3;
            }).AddEntityFrameworkStores<AppDbContext>();
            //services.Configure<IdentityOptions>(options => //override built in configurations...
            //{
            //    options.Password.RequiredLength = 3;

            //});
            services.ConfigureApplicationCookie(options=>options.AccessDeniedPath= new PathString("/Administration/AccessDenied"));
            services.AddMvc().AddXmlSerializerFormatters();
            services.AddAuthorization(options =>
            {
                options.AddPolicy("DeleteRolePolicy", 
                    policy => policy.RequireClaim("Delete Role"));

                options.AddPolicy("EditRolePolicy",
                    policy => policy.RequireAssertion(context=>
                    context.User.IsInRole("Admin") &&
                    context.User.HasClaim(claim=>claim.Type=="Edit Role" && claim.Value=="true") ||
                    context.User.IsInRole("Super Admin")
                    ));

                options.AddPolicy("AdminRolePolicy",
                    policy => policy.RequireClaim("Admin Role"));
            });
            //services.AddMvc(config =>
            //{
            //    var policy = new AuthorizationPolicyBuilder()
            //    .RequireAuthenticatedUser()
            //    .Build();
            //    config.Filters.Add(new AuthorizeFilter(policy));
            //});
            services.AddScoped<IEmployeeRepository, SQLEmployeeRpository>();
            //services.AddMvcCore();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                //{
                //    SourceCodeLineCount=20
                //};
                //app.UseDeveloperExceptionPage(developerExceptionPageOptions);
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
            //FileServerOptions fileServerOptions = new FileServerOptions();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            //fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("foo.html");
            ////app.UseDefaultFiles(defaultFilesOptions);
            //app.UseMvcWithDefaultRoute();
            //app.UseFileServer(fileServerOptions);
            //app.UseFileServer();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello world");
            //});
        }
    }
}
