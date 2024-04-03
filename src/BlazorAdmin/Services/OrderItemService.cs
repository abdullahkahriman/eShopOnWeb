using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;


namespace BlazorAdmin.Services;

public class OrderItemService : IOrderItemService
{
    private readonly HttpService _httpService;
    private readonly ILogger<OrderItemService> _logger;

    public OrderItemService(
        HttpService httpService,
        ILogger<OrderItemService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<List<OrderItemDetail>> GetById(int id)
    {
        var itemGetTask = _httpService.HttpGet<EditOrderItemDetailResult>($"order-items/{id}");
        await Task.WhenAll(itemGetTask);
        var result = itemGetTask.Result.OrderItemDetails;
        return result;
    }

    public async Task<List<OrderItem>> ListPaged(int pageSize)
    {
        _logger.LogInformation("Fetching catalog items from API.");

        var itemListTask = _httpService.HttpGet<PagedOrderItemResponse>($"order-items?PageSize=10");
        var items = itemListTask.Result.OrderItems;
        
        return items;
    }

    public async Task<bool> UpdateStatus(UpdateOrderItemStatusRequest orderItem)
    {
        return (await _httpService.HttpPut<UpdateOrderItemStatusResponse>("update-status", orderItem)).IsSuccess;
    }

    public async Task<List<OrderItem>> List()
    {
        _logger.LogInformation("Fetching order items from API.");

        var itemListTask = _httpService.HttpGet<PagedOrderItemResponse>($"order-items");
        await Task.WhenAll(itemListTask);
        var items = itemListTask.Result.OrderItems;
        
        return items;
    }
}
