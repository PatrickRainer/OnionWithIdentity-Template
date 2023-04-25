using System;
using System.Text;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Repositories;
using Presentation;
using Services;
using Services.Abstractions;
using Web.DbInitializing;
using Web.Middleware;

namespace Web;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(AssemblyReference).Assembly);

        //INFO: AddNewtonsoftJson should used probably. may be issues with mapster, check before you do it
        /*.AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        });*/

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please Bearer and then token in the field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        services.AddScoped<IServiceManager, ServiceManager>();

        services.AddScoped<IRepositoryManager, RepositoryManager>();

        services.AddDbContextPool<RepositoryDbContext>(builder =>
        {
            var connectionString = Configuration.GetConnectionString("Database");

            builder.UseSqlServer(connectionString);
        });

        // Identity
        services.AddIdentity<User, IdentityRole>(opt =>
            {
                // Password settings
                opt.Password.RequiredLength = 14;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;

                // Lockout settings.
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.AllowedForNewUsers = true;

                // User settings.
                opt.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = false;
            })
            .AddEntityFrameworkStores<RepositoryDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        // Authentication
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(jwt =>
        {
            jwt.RequireHttpsMetadata = false;
            jwt.SaveToken = true;
            var apiSecretKey = Configuration.GetValue<string>("ApiSecretKey");
            var validAudience = Configuration.GetValue<string>("ApiValidAudience");
            var validIssuer = Configuration.GetValue<string>("ApiValidIssuer");
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(apiSecretKey)),
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidAudience = validAudience,
                ValidIssuer = validIssuer,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddAuthorization();

        //For more complex Policies
        /*services.AddAuthorization(options =>
        {
            options.AddPolicy("Administrator", policy => policy.RequireRole("Administrator"));
            options.AddPolicy("Visitor", policy => policy.RequireRole("Visitor"));
        });*/

        #region Security Settings

        services.AddCors(o => o.AddPolicy("WebPolicy",
            policyBuilder => { policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }));

        #endregion

        services.AddTransient<ExceptionHandlingMiddleware>();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager)
    {
        // Init Db
        DbInitializer.SeedUsers(userManager);


        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();

            app.UseSwagger();

            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web v1"));
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors("WebPolicy");

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}