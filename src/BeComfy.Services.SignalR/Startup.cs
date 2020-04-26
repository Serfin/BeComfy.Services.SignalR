using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BeComfy.Common.Authentication;
using BeComfy.Common.CqrsFlow;
using BeComfy.Common.CqrsFlow.Handlers;
using BeComfy.Common.RabbitMq;
using BeComfy.Logging.Elk;
using BeComfy.Services.SignalR.Hubs;
using BeComfy.Services.SignalR.HubServices;
using BeComfy.Services.SignalR.OperationHandler;
using BeComfy.Services.SignalR.Operations.Messages.Customers;
using BeComfy.Services.SignalR.Operations.Messages.Tickets;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BeComfy.Services.SignalR
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public IConfiguration Configuration { get; }
        public IContainer Container { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddJwt();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cors => 
                        cors.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });
            services.AddSignalR();
            services.AddTransient<IHubWrapper, HubWrapper>();
            services.AddTransient<IHubMessageManager, HubMessageManager>();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.AddHandlers();
            builder.AddDispatcher();
            builder.AddRabbitMq();

            Container = builder.Build();

            return new AutofacServiceProvider(Container);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }      

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/Home", async context =>
                {
                    await context.Response.WriteAsync("BeComfy SignalR Homepage");
                });
                
                endpoints.MapHub<ClientProcessHub>("/processHub");
            });

            app.UseRabbitMq()
                .SubscribeEvent<TicketBought>()
                .SubscribeEvent<BuyTicketRejected>()
                .SubscribeEvent<CreateCustomerRejected>();
        }
    }
}
