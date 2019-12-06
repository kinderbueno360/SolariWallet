using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Solari.Wallet.Application.Projections;
using Solari.Wallet.Application.Services;
using SqlStreamStore;

namespace Solari.Wallet.Service
{
    public class WalletsService : Wallets.WalletsBase
    {
        private readonly ILogger<WalletsService> _logger;
        private static IStreamStore _streamStore;
        private static IBalanceProjection _balanceProjection;
        private static IAccount _account;

        public WalletsService(IStreamStore streamStore, IAccount account, IBalanceProjection balanceProjection, ILogger<WalletsService> logger)
        {
            _logger = logger;
            _account = account;
            _balanceProjection = balanceProjection;
            _streamStore = streamStore;
        }

        public override Task<WalletReply> Balance(BalanceRequest request, ServerCallContext context)
        {
            return Task.FromResult(new WalletReply
            {
                Message = " Balance: " + _balanceProjection.GetBalance().Amount.ToString()
            });
        }
        public override async Task<WalletReply> Deposit(WalletRequest request, ServerCallContext context)
        {
            await _account.Deposit(Convert.ToDecimal( request.Value));
            return new WalletReply
            {
                Message = " Deposited: " + request.Value
            };
        }
        public override async Task<WalletReply> Withdraw(WalletRequest request, ServerCallContext context)
        {
            await _account.Withdrawal(Convert.ToDecimal(request.Value));
            return new WalletReply
            {
                Message = "Withdrawal: " + request.Value
            };
        }
    }
}
