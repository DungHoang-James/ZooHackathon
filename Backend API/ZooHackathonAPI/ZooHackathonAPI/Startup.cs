using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZooHackathonAPI.DatabaseContext;
using ZooHackathonAPI.Extensions;
using ZooHackathonAPI.Handler;
using ZooHackathonAPI.Repository.BaseRepo;
using ZooHackathonAPI.Repository.ReportImageRepo;
using ZooHackathonAPI.Repository.ReportRepo;
using ZooHackathonAPI.Repository.ReportTextRepo;
using ZooHackathonAPI.Repository.UserRepo;
using ZooHackathonAPI.Services.ReportServices;
using ZooHackathonAPI.Services.StatisticsServices;
using ZooHackathonAPI.Services.UserServices;
using ZooHackathonAPI.UnitOfWorks;

namespace ZooHackathonAPI
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
            services.AddDbContext<ZooDBContext>(option => option.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
            services.AddScoped<DbContext, ZooDBContext>();
            services.AddScoped<ZooDBContext>();

            services.AddHttpClient();

            services.AddRouting();

            // add unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // add automapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // add repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IReportImageRepository, ReportImageRepository>();
            services.AddScoped<IReportTextRepository, ReportTextRepository>();

            // add service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IStatisticService, StatisticService>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("ZooHackathon", new OpenApiInfo { Title = "ZooHackathon API", Version = "v1" });
            });
            services.AddCors();
            services.ConfigureFilter<ErrorHandlingFilter>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/ZooHackathon/swagger.json", "ZooHackathonAPI v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
