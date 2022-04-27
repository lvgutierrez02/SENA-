﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sena.Business;
using Sena.Business.Abstract;
using Sena.Business.Business;
using Sena.DAL;
using Sena.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sena.WEB
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
            services.AddControllersWithViews();
            //Conexión DB


            //Conexión SQl Server
            var conexion = Configuration["ConnectionStrings:conexion_sqlServer"]; //Cadena de conexión
            services.AddDbContext<AppDbContext>(option =>
            option.UseSqlServer(conexion));

            /*
            
            // Conexión MYSQL
            var conexion = Configuration["ConnectionStrings:MySql"]; //Cadena de conexión
                services.AddDbContext<AppDbContext>(option =>
                option.UseMySql(conexion, ServerVersion.AutoDetect(conexion))
            );
            */

            services.AddScoped<IClienteBusiness, ClienteBusiness>();
            services.AddScoped<ITipoDocumentoBusiness, TipoDocumentoBusiness>();
            services.AddScoped<IUsuarioBusiness, UsuarioBusiness>();


            //Indentity

            services.AddIdentity<Usuario, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
              .AddDefaultUI()
              .AddDefaultTokenProviders() //para trabajar con la confirmaci�n de email
              .AddEntityFrameworkStores<AppDbContext>();
            //.AddClaimsPrincipalFactory<UsuarioClaimsPrincipalFactory>();

            //configuraci�n del password
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.User.RequireUniqueEmail = true;
            });


            services.ConfigureApplicationCookie(options =>
            {
                options.AccessDeniedPath = new PathString("/Admin/NoAutorizado");
                options.Cookie.Name = "Cookie";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(720);
                options.LoginPath = new PathString("/Usuarios/Login");
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
                options.SlidingExpiration = true;
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Clientes}/{action=Index}/{id?}");
            });
        }
    }
}
