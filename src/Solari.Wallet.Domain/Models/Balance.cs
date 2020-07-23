using CSharpFunctionalExtensions;
using Solari.Wallet.Domain.Core.Models;
using System;
using System.Collections.Generic;

namespace Solari.Wallet.Domain.Models
{
    public class Balance : ValueObject<Balance>
    {
        public Money Amount { get; protected set; }
        public DateTime AsOf { get; protected set; }

        public static Balance None = new Balance(Money.None, DateTime.Now);

        public static Balance CreateNewBalance(Money amount, DateTime asOf)
        {
            return new Balance(amount, asOf);
        }

        private Balance(Money amount, DateTime asOf)
        {
            Amount = amount;
            AsOf = asOf;
        }

        public Result<Balance> Add(Money moneyAdd)
        {
            SetAsOf();

            return Amount
                    .Add(moneyAdd)
                    .OnFailure(error => Result.Failure<Balance>(error))
                    .Map(money => new Balance(money, AsOf));
        }

        private void SetAsOf()
        {
            AsOf = DateTime.UtcNow;
        }

        public Result<Balance> Subtract(Money moneyAdd)
        {
            AsOf = DateTime.UtcNow;

            return Amount
                    .Subtract(moneyAdd)
                    .OnFailure(error => Result.Failure<Balance>(error))
                    .Map(money => new Balance(money, AsOf));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return AsOf;
        }
    }
}
