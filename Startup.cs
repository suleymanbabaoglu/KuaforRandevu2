using KuaforRandevu2.Models.DbContexts;
using KuaforRandevu2.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace KuaforRandevu2
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));

            services.AddAuthentication()
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
                    cfg =>
                    {
                        cfg.SlidingExpiration = false;
                        cfg.ExpireTimeSpan = new TimeSpan(10000000000);
                        cfg.AccessDeniedPath = "/FrontSide";
                        cfg.LoginPath = "/Login";
                        cfg.LogoutPath = "/Login";
                    });


            services.AddAuthorization();

            services.AddMvc(m => m.EnableEndpointRouting = false).SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Latest);

            services.AddDbContext<KuaforWebContext>(options =>
            {
                //options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnectionString"]);
                //options.UseSqlServer(Configuration["ConnectionStrings:HomeConnectionString"]);
                options.UseMySql(Configuration["ConnectionStrings:MysqlConnectionString"]);
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();               
            }
            else
            {
                app.UseExceptionHandler("/FrontSide/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
              {
                  routes.MapRoute(
                      name: "default",
                      template: "{controller=FrontSide}/{action=Index}/{id?}");
              });
        }
    }
}
