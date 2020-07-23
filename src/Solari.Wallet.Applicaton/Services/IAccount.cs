using System;
using System.Threading.Tasks;

namespace Solari.Wallet.Application.Services
{
    public interface IAccount
    {
        Task<Guid> Deposit(decimal amount);
        Task<Guid> Withdrawal(decimal amount);
        Task Transactions();

    }
}