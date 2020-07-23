namespace Solari.Wallet.Domain.Core
{
    public interface IValidationRule
    {
        bool IsBroken();

        string Message { get; }
    }
}