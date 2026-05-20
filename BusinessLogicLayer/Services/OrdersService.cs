using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using FluentValidation;
using MongoDB.Driver;

namespace BusinessLogicLayer.Services
{
    public class OrdersService(
        IOrdersRepository ordersRepository,
        IMapper mapper,
        IValidator<OrderAddRequest> orderAddRequestValidator,
        IValidator<OrderItemAddRequest> orderItemAddRequestValidator,
        IValidator<OrderUpdateRequest> orderUpdateRequestValidator,
        IValidator<OrderItemUpdateRequest> orderItemUpdateRequestValidator
    ) : IOrdersService
    {
        private readonly IValidator<OrderAddRequest> _orderAddRequestValidator =
            orderAddRequestValidator;
        private readonly IValidator<OrderItemAddRequest> _orderItemAddRequestValidator =
            orderItemAddRequestValidator;
        private readonly IValidator<OrderUpdateRequest> _orderUpdateRequestValidator =
            orderUpdateRequestValidator;
        private readonly IValidator<OrderItemUpdateRequest> _orderItemUpdateRequestValidator =
            orderItemUpdateRequestValidator;
        private readonly IMapper _mapper = mapper;
        private IOrdersRepository _ordersRepository = ordersRepository;

        public async Task<OrderResponse?> AddOrder(OrderAddRequest orderAddRequest)
        {
            var validationResult = await _orderAddRequestValidator.ValidateAsync(orderAddRequest);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            foreach (var item in orderAddRequest.OrderItems)
            {
                var itemValidationResult = await _orderItemAddRequestValidator.ValidateAsync(item);
                if (!itemValidationResult.IsValid)
                {
                    throw new ValidationException(itemValidationResult.Errors);
                }
            }

            var orderEntity = _mapper.Map<Order>(orderAddRequest);
            var addedOrder = await _ordersRepository.AddOrder(orderEntity);
            return _mapper.Map<OrderResponse?>(addedOrder);
        }

        public async Task<bool> DeleteOrder(Guid orderID)
        {
            if (orderID == Guid.Empty)
            {
                throw new ArgumentException("OrderID cannot be empty.", nameof(orderID));
            }
            return await _ordersRepository.DeleteOrder(orderID);
        }

        public async Task<OrderResponse?> GetOrderByCondition(FilterDefinition<Order> filter)
        {
            var order = await _ordersRepository.GetOrderByCondition(filter);
            return _mapper.Map<OrderResponse?>(order);
        }

        public async Task<List<OrderResponse?>> GetOrders()
        {
            var orders = await _ordersRepository.GetOrders();
            return orders.Select(_mapper.Map<OrderResponse?>).ToList();
        }

        public async Task<List<OrderResponse?>> GetOrdersByCondition(FilterDefinition<Order> filter)
        {
            var orders = await _ordersRepository.GetOrdersByCondition(filter);
            return [.. orders.Select(_mapper.Map<OrderResponse?>)];
        }

        public async Task<OrderResponse?> UpdateOrder(OrderUpdateRequest orderUpdateRequest)
        {
            var validationResult = await _orderUpdateRequestValidator.ValidateAsync(
                orderUpdateRequest
            );
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            foreach (var item in orderUpdateRequest.OrderItems)
            {
                var itemValidationResult = await _orderItemUpdateRequestValidator.ValidateAsync(
                    item
                );
                if (!itemValidationResult.IsValid)
                {
                    throw new ValidationException(itemValidationResult.Errors);
                }
            }

            var orderEntity = _mapper.Map<Order>(orderUpdateRequest);
            var updatedOrder = await _ordersRepository.UpdateOrder(orderEntity);
            return _mapper.Map<OrderResponse?>(updatedOrder);
        }
    }
}
