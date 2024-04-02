using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;

public class OrderWithItemsSpec : Specification<Order>
{
    public OrderWithItemsSpec()
    {
        Query
            .Include(o => o.OrderItems)
            .ThenInclude(i => i.ItemOrdered);
    }
}
