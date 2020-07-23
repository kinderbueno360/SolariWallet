using Solari.Wallet.Domain.Core.Commands;
using Solari.Wallet.Domain.Core.Events;
using System.Threading.Tasks;

namespace Solari.Wallet.Domain.Core.Bus
{
    public interface IMediatorHandler
    {
        Task SendCommand<T>(T command) where T : Command;
        Task RaiseEvent<T>(T @event) where T : Event;
    }
}
