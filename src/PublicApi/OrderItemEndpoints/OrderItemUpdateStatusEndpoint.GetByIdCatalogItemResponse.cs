using System;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class UpdateStatusOrderItemResponse : BaseResponse
{
    public UpdateStatusOrderItemResponse(Guid correlationId) : base(correlationId)
    {
    }

    public UpdateStatusOrderItemResponse()
    {
    }

    public bool IsSuccess { get; set; } = new();
}
