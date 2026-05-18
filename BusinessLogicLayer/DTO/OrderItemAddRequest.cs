using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTO
{
    public record OrderItemAddRequest(Guid ProductID, decimal UnitPrice, int Quantity)
    {
        public OrderItemAddRequest()
            : this(default, default, default) { }
    }
}
