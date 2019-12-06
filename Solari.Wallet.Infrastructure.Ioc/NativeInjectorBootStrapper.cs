using Microsoft.Extensions.DependencyInjection;
using Solari.Wallet.Application.Projections;
using Solari.Wallet.Application.Services;
using SqlStreamStore;
using SqlStreamStore.Streams;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solari.Wallet.Infrastructure.Crosscuting.Ioc
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IStreamStore, InMemoryStreamStore>();
            services.AddSingleton<IAccount, Account>((provider) => {
                return new Account(provider.GetService<IStreamStore>(), new StreamId($"Account:BD57ACB6-0DEA-4CEB-8F84-97BC9ED2CBCE"));
            });
            services.AddSingleton<IBalanceProjection, BalanceProjection>((provider) => {
                return new BalanceProjection(provider.GetService<IStreamStore>(), new StreamId($"Account:BD57ACB6-0DEA-4CEB-8F84-97BC9ED2CBCE"));
            });
        }

    }
    

}
