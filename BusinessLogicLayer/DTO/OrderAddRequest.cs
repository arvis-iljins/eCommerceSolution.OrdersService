namespace BusinessLogicLayer.DTO
{
    public class OrderAddRequest(
        Guid UserId,
        DateTime OrderDate,
        List<OrderItemAddRequest> OrderItems
    )
    {
        public OrderAddRequest()
            : this(default, default, default) { }
    }
}
