﻿namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class OrderItemDetailDto
{
    public int Id { get; set; }
    public string Name { get;  set; }
    public decimal Price { get;  set; }
    public int Unit { get;  set; }
}
