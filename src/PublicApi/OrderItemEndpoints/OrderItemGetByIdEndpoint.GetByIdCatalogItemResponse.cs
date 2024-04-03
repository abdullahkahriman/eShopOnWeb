using System;
using System.Collections.Generic;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class GetByIdOrderItemResponse : BaseResponse
{
    public GetByIdOrderItemResponse(Guid correlationId) : base(correlationId)
    {
    }

    public GetByIdOrderItemResponse()
    {
    }

    public List<OrderItemDetailDto> OrderItemDetails { get; set; } = new();
}
