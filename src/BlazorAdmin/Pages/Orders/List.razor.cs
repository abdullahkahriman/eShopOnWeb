using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorAdmin.Helpers;
using BlazorShared.Interfaces;
using BlazorShared.Models;

namespace BlazorAdmin.Pages.OrderPage;

public partial class List : BlazorComponent
{
    [Microsoft.AspNetCore.Components.Inject]
    public IOrderItemService OrderItemService { get; set; }

    private List<OrderItem> orderItems = new();

    private Details DetailsComponent { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            orderItems = await OrderItemService.List();

            CallRequestRefresh();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task ReloadCatalogItems()
    {
        orderItems = await OrderItemService.List();
        StateHasChanged();
    }

    private async void DetailsClick(int id)
    {
        await DetailsComponent.Open(id);
    }
}
