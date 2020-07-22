namespace Solari.Wallet.Domain.Models
{
    using CSharpFunctionalExtensions;
    using Solari.Wallet.Domain.Core.Models;
    using System.Collections.Generic;

    public interface ICurrencyLookup
    {
        Currency FindCurrency(string currencyCode);
    }
    public class Currency : ValueObject<Currency>
    {
        public string CurrencyCode { get; set; }
        public bool InUse { get; set; }
        public int DecimalPlaces { get; set; }

        public static Currency None = new Currency { InUse = false };


        protected override bool EqualsCore(Currency other)
        {
            throw new System.NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new System.NotImplementedException();
        }
    }
}