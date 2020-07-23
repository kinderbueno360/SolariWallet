using Solari.Wallet.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Solari.Wallet.Domain.Events
{
    public class AccountEvent : Event
    {
        public Guid TransactionId { get; }
        public decimal Amount { get; }
        public DateTime DateTime { get; }

        public AccountEvent(Guid transactionId, decimal amount) 
        {
            TransactionId = transactionId;
            Amount = amount;
        }
    }
}
