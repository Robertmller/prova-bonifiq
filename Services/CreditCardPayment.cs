using System.Threading.Tasks;
using ProvaPub.Services;

public class CreditCardPayment : IPaymentMethod
{
    public async Task PayAsync(decimal paymentValue, int customerId)
    {
        // Lógica específica de pagamento via cartão de crédito
        await Task.CompletedTask;
    }
}