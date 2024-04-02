using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;


namespace BlazorAdmin.Services;

public class OrderItemService : IOrderItemService
{
    //private readonly IRepository<Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate.Order> _orderRepository;

    private readonly HttpService _httpService;
    private readonly ILogger<CatalogItemService> _logger;

    public OrderItemService(
        HttpService httpService,
        ILogger<CatalogItemService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public async Task<OrderItem> GetById(int id)
    {
        //var brandListTask = _brandService.List();
        //var typeListTask = _typeService.List();
        var itemGetTask = _httpService.HttpGet<EditOrderItemResult>($"order-items/{id}");
        //await Task.WhenAll(brandListTask, typeListTask, itemGetTask);
        //var brands = brandListTask.Result;
        //var types = typeListTask.Result;
        var catalogItem = itemGetTask.Result.OrderItem;
        return catalogItem;
    }

    public async Task<List<OrderItem>> ListPaged(int pageSize)
    {
        _logger.LogInformation("Fetching catalog items from API.");

        //var brandListTask = _brandService.List();
        //var typeListTask = _typeService.List();
        var itemListTask = _httpService.HttpGet<PagedOrderItemResponse>($"order-items?PageSize=10");
        //await Task.WhenAll(brandListTask, typeListTask, itemListTask);
        //var brands = brandListTask.Result;
        //var types = typeListTask.Result;
        var items = itemListTask.Result.OrderItems;
        
        return items;
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
