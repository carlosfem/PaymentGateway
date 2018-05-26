using System.ComponentModel;

/// <summary>
/// Enum with the credit card brand options.
/// </summary>
namespace PaymentGateway.Model.Entity
{
    enum CreditCardBrandEnum
    {
        [Description("Elo")]
        Elo         = 1,
        Visa        = 2,
        Hipercard   = 3,
        Mastercard  = 4
    }
}
