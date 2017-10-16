using AutoMapper;
using CaseTracker.Core;
using CaseTracker.Core.Models;
using CaseTracker.Core.Repositories;
using CaseTracker.Core.Services;
using CaseTracker.Data;
using CaseTracker.Data.Repositories;
using CaseTracker.Portal.Mapping;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CaseTracker.Portal
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see https://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
              {
                  options.Cookies.ApplicationCookie.AutomaticAuthenticate = true;
                  options.Cookies.ApplicationCookie.AutomaticChallenge = true;
                  options.Cookies.ApplicationCookie.LoginPath = "/account/Login";
              })
                  .AddEntityFrameworkStores<AppDbContext>()
                  .AddDefaultTokenProviders();


            services.AddAuthentication();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("Admin", policy =>
                    policy.RequireClaim("LoginProvider", "SPLC"));
            });


            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            services.AddScoped<IUserService, HttpContextUserService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICaseRepository, CaseRepository>();
            services.AddScoped<ICourtRepository, CourtRepository>();
            services.AddScoped<IJurisdictionRepository, JurisdictionRepository>();
            services.AddScoped<ILitigantRepository, LitigantRepository>();

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, AppDbContext db)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/App/Error");
            }

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseOpenIdConnectAuthentication(new OpenIdConnectOptions()
            {
                AuthenticationScheme = "SPLC",
                Authority = "https://login.microsoftonline.com/common/",
                ResponseType = "code id_token",
                ClientId = Configuration["Authentication:AzureAd:ClientId"],
                ClientSecret = Configuration["Authentication:AzureAd:Password"],
                GetClaimsFromUserInfoEndpoint = true,
                SaveTokens = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                },
                AutomaticChallenge = false,
                AutomaticAuthenticate = false,
            });

            app.UseGoogleAuthentication(new GoogleOptions()
            {
                ClientId = Configuration["Authentication:Google:ClientId"],
                ClientSecret = Configuration["Authentication:Google:ClientSecret"],
            });

            app.UseFacebookAuthentication(new FacebookOptions()
            {
                AppId = Configuration["Authentication:Facebook:AppId"],
                AppSecret = Configuration["Authentication:Facebook:AppSecret"],
            });

            app.UseTwitterAuthentication(new TwitterOptions()
            {
                ConsumerKey = Configuration["Authentication:Twitter.ConsumerKey"],
                ConsumerSecret = Configuration["Authentication:Twitter.ConsumerSecret"]
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute("case", "case/{id?}", defaults: new { controller = "App", action = "Case" });
                routes.MapRoute("Courts", "courts", defaults: new { controller = "App", action = "Courts" });
                routes.MapRoute("Jurisidictions", "jurisdictions", defaults: new { controller = "App", action = "Jurisdictions" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=App}/{action=Index}");
            });
        }
    }
}
