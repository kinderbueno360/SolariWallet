using Solari.Wallet.Domain.Models;

namespace Solari.Wallet.Application.Projections
{
    public interface IBalanceProjection
    {
        Balance GetBalance();
    }
}