using System;
using System.Collections.Generic;
using System.Text;

namespace Solari.Wallet.Domain.Events
{
    public class Deposited : AccountEvent
    {
        public Deposited(Guid transactionId, decimal amount) : base(transactionId, amount)
        {

        }
    }
}
