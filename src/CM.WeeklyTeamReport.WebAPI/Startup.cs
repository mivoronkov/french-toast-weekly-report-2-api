using CM.WeeklyTeamReport.Domain;
using CM.WeeklyTeamReport.Domain.Commands;
using CM.WeeklyTeamReport.Domain.Repositories.Implementations;
using CM.WeeklyTeamReport.Domain.Repositories.Interfaces;
using CM.WeeklyTeamReport.Domain.Repositories.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
            services.AddTransient<IGradeRepository, GradeRepository>();
            services.AddTransient<IReportingBetweenMembersRepository, ReportingBetweenTeamMembersRepository>();

            services.AddTransient<ICompanyCommand, CompanyCommand>();
            services.AddTransient<IMemberCommands, MemberCommands>();
            services.AddTransient<IReportCommands, ReportCommands>();

            services.AddTransient<ICompanyManager, CompanyManager>();
            services.AddTransient<ITeamMemberManager, TeamMemberManager>();
            services.AddTransient<IWeeklyReportManager, WeeklyReportManager>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CM.WeeklyTeamReport.WebAPI", Version = "v1" });
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
