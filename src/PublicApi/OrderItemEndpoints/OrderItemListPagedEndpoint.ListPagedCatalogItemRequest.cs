namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class ListPagedOrderItemRequest : BaseRequest
{
    public int PageSize { get; init; }
    public int PageIndex { get; init; }

    public ListPagedOrderItemRequest(int? pageSize, int? pageIndex)
    {
        PageSize = pageSize ?? 0;
        PageIndex = pageIndex ?? 0;
    }
}
