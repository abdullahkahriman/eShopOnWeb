using System;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class OrderItemDto
{
    public int Id { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public string Username { get; set; }
    public decimal TotalPrice { get; set; }
    public string StatusName { get; set; }
}
