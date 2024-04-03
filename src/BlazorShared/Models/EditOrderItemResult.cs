using System.Collections.Generic;

namespace BlazorShared.Models;

public class EditOrderItemResult
{
    public OrderItem OrderItem { get; set; } = new();
}

public class EditOrderItemDetailResult
{
    public List<OrderItemDetail> OrderItemDetails { get; set; } = new();
}
