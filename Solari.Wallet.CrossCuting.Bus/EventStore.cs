using MediatR;
using Solari.Wallet.Domain.Core.Bus;
using Solari.Wallet.Domain.Core.Commands;
using Solari.Wallet.Domain.Core.Events;
using SqlStreamStore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Solari.Wallet.CrossCuting.Bus
{
    public sealed class EventStore : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IStreamStore _eventStore;

        public EventStore(IStreamStore eventStore, IMediator mediator)
        {
            _eventStore = eventStore;
            _mediator = mediator;
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public Task RaiseEvent<T>(T @event) where T : Event
        {
            //if (!@event.MessageType.Equals("DomainNotification"))
                //_eventStore?.AppendToStream(@event);

            return _mediator.Publish(@event);
        }
    }
}
