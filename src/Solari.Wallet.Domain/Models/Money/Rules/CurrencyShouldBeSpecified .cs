using Solari.Wallet.Domain.Core;

namespace Solari.Wallet.Domain.Models
{
    public class CurrencyShouldBeSpecified : IValidationRule
    {
        private readonly string _value;

        internal CurrencyShouldBeSpecified(string value)
        {
            this._value = value;
        }

        public string Message => "Currency code must be specified";

        public bool IsBroken()
        {
            return (string.IsNullOrEmpty(_value));
        }
    }
}