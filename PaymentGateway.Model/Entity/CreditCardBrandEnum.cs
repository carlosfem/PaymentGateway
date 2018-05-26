using System.ComponentModel;

/// <summary>
/// Enum with the credit card brand options.
/// </summary>
namespace PaymentGateway.Model.Entity
{
    enum CreditCardBrandEnum
    {
        [Description("Elo")]
        Elo = 1,
        [Description("Visa")]
        Visa = 2,
        [Description("Hipercard")]
        Hipercard = 3,
        [Description("Mastercard")]
        Mastercard = 4
    }
}
