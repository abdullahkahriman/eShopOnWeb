using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;

public interface IOrderItemService
{
    Task<OrderItem> GetById(int id);
    Task<List<OrderItem>> ListPaged(int pageSize);
    Task<List<OrderItem>> List();
}
