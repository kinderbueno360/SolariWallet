using System;
using System.Collections.Generic;
using System.Text;

namespace Solari.Wallet.Domain.Events
{
    public class Withdrawn : AccountEvent
    {
        public Withdrawn(Guid transactionId, decimal amount) : base(transactionId, amount)
        {

        }
    }
}
