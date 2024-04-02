using System;

namespace BlazorShared.Models;

public class OrderItem
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public string Username { get; set; }
    public decimal TotalPrice { get; set; }
    public decimal UnitPrice { get; set; }
    public int Units { get; set; }
    public string StatusName { get; set; }
}
