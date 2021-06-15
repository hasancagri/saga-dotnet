using EventBus.Messages.Common;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StockService.API.Consumer;
using StockService.API.Services;

namespace StockService.API
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
            services.AddMassTransit(config =>
            {
                services.AddScoped<IService, Service>();
                config.AddConsumer<OrderCreatedEventConsumer>();
                config.AddConsumer<PaymentCompletedEventConsumer>();
                config.AddConsumer<PaymentRejectedEventConsumer>();
                config.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(Configuration["EventBusSettings:HostAddress"]);
                    cfg.ReceiveEndpoint(EventBusConstants.OrderCreatedEvent, c =>
                    {
                        c.ConfigureConsumer<OrderCreatedEventConsumer>(ctx);
                    });

                    cfg.ReceiveEndpoint(EventBusConstants.PaymentCompletedEvent, c =>
                    {
                        c.ConfigureConsumer<PaymentCompletedEventConsumer>(ctx);
                    });

                    cfg.ReceiveEndpoint(EventBusConstants.PaymentRejectedEvent, c =>
                    {
                        c.ConfigureConsumer<PaymentRejectedEventConsumer>(ctx);
                    });
                });
            });
            services.AddMassTransitHostedService();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "StockService.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StockService.API v1"));
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
