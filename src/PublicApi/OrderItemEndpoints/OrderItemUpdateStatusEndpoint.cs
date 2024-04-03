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
/// Update order status
/// </summary>
public class OrderItemUpdateStatusEndpoint : IEndpoint<IResult, UpdateStatusOrderItemRequest, IRepository<Order>>
{
    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/update-status",
            async (UpdateStatusOrderItemRequest request, IRepository <Order> itemRepository) =>
            {
                return await HandleAsync(request, itemRepository);
            })
            .Produces<GetByIdOrderItemResponse>()
            .WithTags("OrderItemEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateStatusOrderItemRequest request, IRepository<Order> itemRepository)
    {
        var response = new UpdateStatusOrderItemResponse(request.CorrelationId());

        OrderWithItemsByIdSpec filterSpec = new(request.Id);
        var item = await itemRepository.FirstOrDefaultAsync(filterSpec);
        if (item is null || item.OrderItems?.Count <= 0)
            response.IsSuccess = false;
        else
        {
            item.SetStatus(OrderStatus.Delivered);
            await itemRepository.UpdateAsync(item);
            await itemRepository.SaveChangesAsync();

            response.IsSuccess = true;
        }

        return Results.Ok(response);
    }
}
