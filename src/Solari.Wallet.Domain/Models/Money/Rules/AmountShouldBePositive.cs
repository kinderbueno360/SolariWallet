namespace Solari.Wallet.Domain.Models
{
    using System;
    using Solari.Wallet.Domain.Core;

    public class AmountShouldBePositive : IValidationRule
    {
        private readonly Decimal _value;

        internal AmountShouldBePositive(Decimal value)
        {
            this._value = value;
        }

        public string Message => "Amount should be positive";

        public bool IsBroken()
        {
            return (!(_value >= 0));
        }
    }
}