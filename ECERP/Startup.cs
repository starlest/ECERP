﻿namespace ECERP.API
{
    using AspNet.Security.OpenIdConnect.Primitives;
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.IdentityModel.Tokens;
    using Models.Entities;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add a reference to the Configuration object for DI
            services.AddSingleton<IConfiguration>(c => Configuration);

            services.AddMvc();

            // Add EntityFramework's Identity support.
            services.AddEntityFramework();

            // Add Identity Services & Stores
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
                {
                    config.User.RequireUniqueEmail = true;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Cookies.ApplicationCookie.AutomaticChallenge = false;
                }).AddEntityFrameworkStores<ECERPDbContext>()
                .AddDefaultTokenProviders();

            // Add ApplicationDbContext.
            services.AddDbContext<ECERPDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"],
                        b => b.MigrationsAssembly("ECERP.API"));
                    // Register the entity sets needed by OpenIddict.
                    // Note: use the generic overload if you need
                    // to replace the default OpenIddict entities.
                    options.UseOpenIddict();
                });


            // Register the OpenIddict services.
            services.AddOpenIddict()
                // Register the Entity Framework stores.
                .AddEntityFrameworkCoreStores<ECERPDbContext>()
                // Register the ASP.NET Core MVC binder used by OpenIddict.
                // Note: if you don't call this method, you won't be able to
                // bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
                .AddMvcBinders()
                // Use Json Web Tokens (JWT)
                .UseJsonWebTokens()
                // Set a custom token endpoint (default is /connect/token)
                .EnableTokenEndpoint(Configuration["Authentication:OpenIddict:TokenEndPoint"])
                // Set a custom auth endpoint (default is connect/authorize)
                .EnableAuthorizationEndpoint(Configuration["Authentication:OpenIddict:AuthorizationEndPoint"])
                // Allow client applications to use the grant_type=password flow.
                .AllowPasswordFlow()
                // Enable support for both authorization and implicit flows
                .AllowAuthorizationCodeFlow()
                .AllowImplicitFlow()
                // Allow the client to refresh tokens.
                .AllowRefreshTokenFlow()
                // Disable the HTTPS requirement (not recommended in production)
                .DisableHttpsRequirement()
                // Register a new ephemeral key for development.
                // We will register a X.509 certificate in production.
                .AddEphemeralSigningKey();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Add the AspNetCore.Identity middleware (required for external auth providers)
            // IMPORTANT: This must be placed *BEFORE* OpenIddict and any external provider's middleware
            app.UseIdentity();

            // Add OpenIddict middleware
            // Note: UseOpenIddict() must be registered after app.UseIdentity() and the external social providers
            app.UseOpenIddict();

            // Add the Jwt Bearer Header Authentication to validate Tokens
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,
                Authority = Configuration["Authentication:OpenIddict:Authority"],
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false
                }
            });

            // Add MVC to the pipeline
            app.UseMvc();
        }
    }
}