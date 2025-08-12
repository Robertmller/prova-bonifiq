using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;

public class OrderService
{
    private readonly TestDbContext _ctx;
    private readonly IDictionary<string, IPaymentMethod> _paymentMethods;

    public OrderService(TestDbContext ctx)
    {
        _ctx = ctx;

        // Registrar os métodos disponíveis
        _paymentMethods = new Dictionary<string, IPaymentMethod>(StringComparer.OrdinalIgnoreCase)
        {
            { "pix", new PixPayment() },
            { "creditcard", new CreditCardPayment() },
            { "paypal", new PaypalPayment() }
        };
    }

    public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
    {
        if (customerId <= 0)
            throw new ArgumentException("CustomerId inválido.");

        if (!_paymentMethods.TryGetValue(paymentMethod, out var paymentProcessor))
            throw new ArgumentException("Método de pagamento inválido.");

        await paymentProcessor.PayAsync(paymentValue, customerId);

        var order = new Order()
        {
            Value = paymentValue,
            CustomerId = customerId,
            OrderDate = DateTime.UtcNow // salva em UTC
        };

        return await InsertOrder(order);
    }

    public async Task<Order> InsertOrder(Order order)
    {
        var entityEntry = await _ctx.Orders.AddAsync(order);
        await _ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }
}
