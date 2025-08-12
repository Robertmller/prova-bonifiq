
using System.Threading.Tasks;
using ProvaPub.Services;

public class PaypalPayment : IPaymentMethod
{
    public async Task PayAsync(decimal paymentValue, int customerId)
    {
        // Lógica específica de pagamento via Paypal
        await Task.CompletedTask;
    }
}