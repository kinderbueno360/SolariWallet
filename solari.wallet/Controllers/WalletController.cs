using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Net.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Solari.Wallet.Application.Projections;
using Solari.Wallet.Application.Services;
using Solari.Wallet.Service;
using SqlStreamStore;
using SqlStreamStore.Streams;
using static Solari.Wallet.Service.Wallets;

namespace Solari.Wallet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletController : ControllerBase
    {
        private static WalletsClient  _walletsClient;

        public WalletController()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            _walletsClient = new Wallets.WalletsClient(channel);
        }

        [HttpGet]
        public string Get()
        {
            var reply = _walletsClient.Balance(new BalanceRequest());
            return reply.Message;
        }

        [HttpGet]
        [Route("deposit/{value}")]
        public async Task<string> Deposit(int value)
        {
            var reply = await _walletsClient.DepositAsync(
                              new WalletRequest { Value = value });

            return reply.Message;
        }

        [HttpGet]
        [Route("withdraw/{value}")]
        public async Task<string> Withdraw(int value)
        {
            var reply = await _walletsClient.WithdrawAsync(
                              new WalletRequest { Value = value });

            return reply.Message + " Deposited: " + value;
        }

    }
}
