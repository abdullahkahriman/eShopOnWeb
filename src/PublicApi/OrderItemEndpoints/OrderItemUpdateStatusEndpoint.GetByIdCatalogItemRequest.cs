using BlazorShared;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class UpdateStatusOrderItemRequest : BaseRequest
{
    public int Id { get; set; }
    public OrderStatus StatusName { get; set; }
}
