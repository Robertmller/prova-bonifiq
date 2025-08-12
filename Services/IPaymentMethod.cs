using System.Threading.Tasks;

public interface IPaymentMethod
{
    Task PayAsync(decimal paymentValue, int customerId);
}
