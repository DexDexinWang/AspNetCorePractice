using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DexCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DexCore
{

    /// <summary>
    /// No interface, no inheritance
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            // var dexCore = _configuration["DexCore:BoldDepartmentEmployeeCountThreshold"];
        }

        /// <summary>
        /// Call First
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //Configuration Dependency Injection
            //Inversion of control  - Registration, initialization, setup lifecycle for initied object
            //Lifecycle: 
            //1. Transient -- create object for each request 
            //2. Scoped -- create ojbect for each request and will be released after the request 
            //3. singleton -- Only one object when running the service

            services.AddControllersWithViews(); //support controllers API features and views but no views. (MVC)
            services.AddSingleton<IClock, ChinaClock>(); // create service lifecycle, it will return ChinaClock if input is IClock

            //Advantage: 
            //1. no strong independent (Decoupling)
            //2. No Necessary to understand detailed implementation
            //3. No lifecycle management

            services.AddSingleton<IDepartmentService, DepartmentService>();
            services.AddSingleton<IEmployeeService, EmployeeService>();

            services.Configure<DexCoreOptions>(_configuration.GetSection("DexCore"));
        }

        /// <summary>
        /// Second execution as a midware. 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 1st midware. environment is defined in appsetting.Development.json.
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage(); //If developing and has error, then show exception page. 
            }


            //Use static files such as javascript.
            app.UseStaticFiles();

            //convert http request as https request for SSL constrain
            app.UseHttpsRedirection();

            app.UseAuthentication();

            //2nd midware
            //define routing to manipulate the endpoint of url. Normally, there are 2 ways:
            //1. /{controller}/{action}
            //2. /home/index (with default template)

            //MVC:/home/index
            //Razor Pages: /somepage
            //SignalR: /Hub/Chat
            app.UseRouting();  

            app.UseEndpoints(endpoints =>
            {
                //define template for rounting.
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern:"{controller=Department}/{action=Index}/{id?}"
                    );

                // if adding attributes to controller to create routing. 
                //endpoints.MapControllers(); 

                /*
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
                */
            });
        }
    }
}
