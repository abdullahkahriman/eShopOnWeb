using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

/// <summary>
/// Get a Order Item by Id
/// </summary>
public class OrderItemGetByIdEndpoint : IEndpoint<IResult, GetByIdOrderItemRequest, IRepository<Order>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/order-items/{orderId}",
            async (int orderId, IRepository<Order> itemRepository) =>
            {
                return await HandleAsync(new GetByIdOrderItemRequest(orderId), itemRepository);
            })
            .Produces<GetByIdOrderItemResponse>()
            .WithTags("OrderItemEndpoints");
    }

    public async Task<IResult> HandleAsync(GetByIdOrderItemRequest request, IRepository<Order> itemRepository)
    {
        var response = new GetByIdOrderItemResponse(request.CorrelationId());

        OrderWithItemsByIdSpec filterSpec = new(request.OrderId);
        var item = await itemRepository.FirstOrDefaultAsync(filterSpec);
        if (item is null || item.OrderItems?.Count <= 0)
            return Results.NotFound();

        response.OrderItemDetails = item.OrderItems.Select(c => new OrderItemDetailDto()
        {
            Id = c.Id,
            Name = c.ItemOrdered.ProductName,
            Price = c.UnitPrice,
            Unit = c.Units
        }).ToList();

        return Results.Ok(response);
    }
}
