using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Managers.Implementations;
using CM.WeeklyTeamReport.Domain.Managers.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Implementations;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Managers;
using CM.WeeklyTeamReport.WebAPI.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

//ncrunch: no coverage start
namespace CM.WeeklyTeamReport.WebAPI
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
            services.AddTransient<ICompanyRepository, CompanyRepository>();
            services.AddTransient<ITeamMemberRepository, TeamMemberRepository>();
            services.AddTransient<IWeeklyReportRepository, WeeklyReportRepository>();
            services.AddTransient<ITeamLinkRepository, TeamLinkRepository>();

            services.AddTransient<ICompanyCommand, CompanyCommand>();
            services.AddTransient<IMemberCommands, MemberCommands>();
            services.AddTransient<IReportCommands, ReportCommands>();
            services.AddTransient<IUserCommands, UserCommands>();

            services.AddTransient<ICompanyManager, CompanyManager>();
            services.AddTransient<ITeamMemberManager, TeamMemberManager>();
            services.AddTransient<IWeeklyReportManager, WeeklyReportManager>();
            services.AddTransient<ITeamLinkManager, TeamLinkManager>();
            services.AddTransient<IUserManager, UserManager>();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("https://weekly-report-02.digitalocean.ankocorp.com",
                                            "https://159.89.32.198",
                                            // For local tests
                                            "https://localhost:5001",
                                            "http://localhost:3000")
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .AllowAnyMethod();
                    });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = $"https://{Configuration["Auth0:Domain"]}/";
                options.Audience = Configuration["Auth0:Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier,
                };
            });

            services.AddControllers(options =>
            {
                options.Filters.Add(new ExceptionFilter());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CM.WeeklyTeamReport.WebAPI", Version = "v1" });

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "Using the Authorization header with the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{ securitySchema, new[] { "Bearer" } }});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CM.WeeklyTeamReport.WebAPI v1"));
            }

            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
