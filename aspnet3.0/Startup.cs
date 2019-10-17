using System.Text;
using aspnet3.Autofac;
using aspnet3.Data;
using Autofac;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
namespace aspnet3
{
    public class Startup
    {
        public Startup(IConfiguration configuration , IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }


        #region snippet_defaultPolicy
        #region snippet
        #region snippet2
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Environment.GetEnvironmentVariable("DefaultConnectionString")
                   ));
            // Configuration.GetConnectionString("DefaultConnection"))
            services.AddDefaultIdentity<IdentityUser>(
                options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders(); 


            #endregion
            services.AddMvc();
            services.AddRazorPages();
            //services.AddSession();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;// CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("1111111111asdfasfdasdf3q45r242"))
                };
            });

            //services.AddControllers(config =>
            //{
            //    //using Microsoft.AspNetCore.Mvc.Authorization;
            //    //using Microsoft.AspNetCore.Authorization;
            //    //var policy = new AuthorizationPolicyBuilder()
            //    //                 .RequireAuthenticatedUser()
            //    //                 .Build();
            //    config.Filters.Add(new AuthorizeFilter());
            //});
            #endregion
            // Authorization handlers.
            //services.AddScoped<IAuthorizationHandler,
            //                      ContactIsOwnerAuthorizationHandler>();

            //services.AddSingleton<IAuthorizationHandler,
            //                      ContactAdministratorsAuthorizationHandler>();

            //services.AddSingleton<IAuthorizationHandler,
            //                      ContactManagerAuthorizationHandler>();



        }
        #endregion

        //public void ConfigureContainer(ContainerBuilder builder)
        //{
        //    builder.RegisterModule(new AutofacModule());
        //}
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //如果没有launchSetting.json(vscode 产生)里面的EnvironmentVariables有一个ASPNETCORE_Environment变量，如果没有这个文件，则默认值为Production
            //如果有这个文件，则以该文件里的变量值为准
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.Run(async (context) =>  {
            //    await context.Response.WriteAsync("自定义环境变量是:" + Environment.GetEnvironmentVariable("DefaultConnectionString"));
            //} );
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            //app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
