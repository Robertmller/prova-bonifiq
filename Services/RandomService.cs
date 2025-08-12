using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class RandomService
    {
        private readonly TestDbContext _ctx;
        private readonly Random _random;

        public RandomService(TestDbContext ctx)
        {
            _ctx = ctx;
            _random = new Random(); // instancia uma vez só, sem seed fixa
        }

        public async Task<int> GetRandom()
        {
            int number;
            const int maxAttempts = 10;
            int attempts = 0;

            do
            {
                number = _random.Next(100);
                bool exists = await _ctx.Numbers.AnyAsync(n => n.Number == number);
                if (!exists)
                {
                    // número único encontrado, salva e retorna
                    _ctx.Numbers.Add(new RandomNumber { Number = number });
                    await _ctx.SaveChangesAsync();
                    return number;
                }

                attempts++;
            } while (attempts < maxAttempts);

            // Caso não consiga encontrar número único depois de 10 tentativas, retorna um erro
            throw new Exception("Não foi possível gerar um número único.");
        }
    }
}
