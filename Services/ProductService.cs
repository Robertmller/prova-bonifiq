using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService
	{
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

        public ProductList ListProducts(int page)
        {
            const int pageSize = 10;
            int totalCount = _ctx.Products.Count();

            // Garantir que page seja >= 1 para evitar valores inválidos
            if (page < 1) page = 1;

            var products = _ctx.Products
                               .OrderBy(p => p.Id) // para ordenação consistente
                               .Skip((page - 1) * pageSize)
                               .Take(pageSize)
                               .ToList();

            bool hasNext = (page * pageSize) < totalCount;

            return new ProductList()
            {
                Products = products,
                TotalCount = totalCount,
                HasNext = hasNext
            };
        }

    }
}
