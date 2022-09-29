using AutoMapper;
using MassTransit;
using MassTransit.Definition;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MT.RabbitMq;
using MT.RabbitMqMessage;
using MT.ReportService.API.Consumers;
using MT.ReportService.Core.Interfaces.Repositories;
using MT.ReportService.Core.Interfaces.Services;
using MT.ReportService.Core.Interfaces.UnitOfWork;
using MT.ReportService.Data.Context;
using MT.ReportService.Data.Repositories;
using MT.ReportService.Data.UnitOfWork;
using MT.ReportService.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MT.ReportService.API
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

            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<IUnitOfWork, UnitOfWork>(); //bir request enasýnda birden fazla ihtiyaç olursa ayný nesne örneðini kullanýr.

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));


            services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("NpgConStr"), o => o.MigrationsAssembly("MT.ReportService.Data")));
            


            services.AddControllers();

            services.TryAddSingleton(KebabCaseEndpointNameFormatter.Instance);
            services.AddMassTransit(cfg =>
            {
                cfg.AddRequestClient<IStartReport>();

                cfg.AddConsumer<StartReportConsumer>();
                cfg.AddConsumer<ReportCancelledConsumer>();

                cfg.AddBus(provider => RabbitMqBus.ConfigureBus(provider));
            });

            services.AddMassTransitHostedService();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MT.ReportService.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MT.ReportService.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
