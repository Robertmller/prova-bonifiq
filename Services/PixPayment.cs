using System.Threading.Tasks;
using ProvaPub.Services;

public class PixPayment : IPaymentMethod
{
    public async Task PayAsync(decimal paymentValue, int customerId)
    {
        // Lógica específica de pagamento via Pix
        await Task.CompletedTask;
    }
}