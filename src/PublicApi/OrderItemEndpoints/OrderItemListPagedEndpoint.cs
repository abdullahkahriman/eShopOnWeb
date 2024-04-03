using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BlazorShared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

/// <summary>
/// List Order Items (paged)
/// </summary>
public class OrderItemListPagedEndpoint : IEndpoint<IResult, ListPagedOrderItemRequest, IRepository<Order>>
{
    private readonly IUriComposer _uriComposer;
    private readonly IMapper _mapper;

    public OrderItemListPagedEndpoint(IUriComposer uriComposer, IMapper mapper)
    {
        _uriComposer = uriComposer;
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/order-items",
            async (int? pageSize, int? pageIndex, IRepository<Order> orderRepository) =>
            {
                return await HandleAsync(new ListPagedOrderItemRequest(pageSize, pageIndex), orderRepository);
            })
            .Produces<ListPagedOrderItemResponse>()
            .WithTags("OrderItemEndpoints");
    }

    public async Task<IResult> HandleAsync(ListPagedOrderItemRequest request, IRepository<Order> orderRepository)
    {
        var response = new ListPagedOrderItemResponse(request.CorrelationId());
        int totalItems = await orderRepository.CountAsync();

        OrderWithItemsSpec filterSpec = new();
        var orders = (await orderRepository.ListAsync(filterSpec)).OrderByDescending(c => c.Id);

        response.OrderItems.AddRange(orders.Select(c => new OrderItemDto()
        {
            Id = c.Id,
            OrderDate = c.OrderDate,
            TotalPrice = c.Total(),
            Username = c.BuyerId,
            StatusName = Enum.GetName(typeof(OrderStatus), c.StatusName)
        }));


        if (request.PageSize > 0)
        {
            response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize).ToString());
        }
        else
        {
            response.PageCount = totalItems > 0 ? 1 : 0;
        }

        return Results.Ok(response);
    }
}
