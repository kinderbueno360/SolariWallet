using Solari.Wallet.Domain.Core;

namespace Solari.Wallet.Domain.Models
{
    public class CurrencySouldBeInUse : IValidationRule
    {
        private readonly string _value;

        private readonly bool _inUser;

        internal CurrencySouldBeInUse(string value, bool inUse)
        {
            this._value = value;
            this._inUser = inUse;
        }

        public string Message => $"Currency {_value} is not valid";

        public bool IsBroken()
        {
            return (!_inUser);
        }
    }
}