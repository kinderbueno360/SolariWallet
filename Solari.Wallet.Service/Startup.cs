using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Solari.Wallet.Application.Projections;
using Solari.Wallet.Application.Services;
using SqlStreamStore;
using SqlStreamStore.Streams;

namespace Solari.Wallet.Service
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
            services.AddSingleton<IStreamStore, InMemoryStreamStore>();
            services.AddSingleton<IAccount, Account>((provider) => {
                return new Account(provider.GetService<IStreamStore>(), new StreamId($"Account:BD57ACB6-0DEA-4CEB-8F84-97BC9ED2CBCE"));
            });
            services.AddSingleton<IBalanceProjection, BalanceProjection>((provider) => {
                return new BalanceProjection(provider.GetService<IStreamStore>(), new StreamId($"Account:BD57ACB6-0DEA-4CEB-8F84-97BC9ED2CBCE"));
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<WalletsService>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }
    }
}
