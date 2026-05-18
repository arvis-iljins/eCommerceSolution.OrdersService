using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public record OrderUpdateRequest(
        Guid OrderID,
        Guid UserID,
        DateTime OrderDate,
        List<OrderItemAddRequest> OrderItems
    )
    {
        public OrderUpdateRequest()
            : this(default, default, default, default) { }
    }
}
