namespace BlazorShared.Models;

public class UpdateOrderItemStatusRequest
{
    public int Id { get; set; }
    public OrderStatus StatusName { get; set; }
}
