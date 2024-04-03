using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;

public interface IOrderItemService
{
    Task<List<OrderItemDetail>> GetById(int id);
    Task<List<OrderItem>> ListPaged(int pageSize);
    Task<bool> UpdateStatus(UpdateOrderItemStatusRequest orderItem);
    Task<List<OrderItem>> List();
}
