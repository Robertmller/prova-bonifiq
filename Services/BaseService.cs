using ProvaPub.Models;
using ProvaPub.Repository;
using Microsoft.EntityFrameworkCore;

namespace ProvaPub.Services
{
    public class BaseService<T> where T : class
    {
        protected readonly TestDbContext _ctx;

        public BaseService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public PagedList<T> ListPaged(int page, int pageSize = 10)
        {
            int totalCount = _ctx.Set<T>().Count();
            if (page < 1) page = 1;

            var items = _ctx.Set<T>()
                            .OrderBy(e => EF.Property<int>(e, "Id"))
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

            bool hasNext = (page * pageSize) < totalCount;

            return new PagedList<T>
            {
                Items = items,
                TotalCount = totalCount,
                HasNext = hasNext
            };
        }
    }
}
