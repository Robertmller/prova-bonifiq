using System.Threading.Tasks;
using ProvaPub.Services;

public class CreditCardPayment : IPaymentMethod
{
    public async Task PayAsync(decimal paymentValue, int customerId)
    {
        // L�gica espec�fica de pagamento via cart�o de cr�dito
        await Task.CompletedTask;
    }
}