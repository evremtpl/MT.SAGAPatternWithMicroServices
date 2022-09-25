using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MT.PersonService.API.Consumers;
using MT.PersonService.Core.Interfaces.Repository;
using MT.PersonService.Core.Interfaces.Services;
using MT.PersonService.Core.Interfaces.UnitOfWork;
using MT.PersonService.Data.Contexts;
using MT.PersonService.Data.Repositories;
using MT.PersonService.Data.UnitOfWork;
using MT.PersonService.Service.Services;
using MT.RabbitMq;

namespace MT.PersonService.API
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
            

            services.AddScoped<IUnitOfWork, UnitOfWork>(); //bir request enasýnda birden fazla ihtiyaç olursa ayný nesne örneðini kullanýr.

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SqlSrvConStr"), o => o.MigrationsAssembly("MT.PersonService.Data")));
            services.AddControllers();

            services.AddMassTransit(cfg =>
            {
                cfg.AddConsumer<ReportValidateConsumer>();

                cfg.AddBus(provider => RabbitMqBus.ConfigureBus(provider, (cfg, host) =>
                {
                }));
            });
            services.AddMassTransitHostedService();



            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MT.PersonService.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MT.PersonService.API v1"));
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
