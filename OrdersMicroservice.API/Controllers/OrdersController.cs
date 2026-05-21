using BusinessLogicLayer.DTO;
using BusinessLogicLayer.ServiceContracts;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace OrdersMicroservice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrdersService _ordersService;

        public OrdersController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        /// <summary>
        /// Gets all orders.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns the list of orders</response>
        /// <response code="500">If there is an internal server error</response>
        /// <response code="400">If the request is invalid</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _ordersService.GetOrders();
            return Ok(orders);
        }

        /// <summary>
        /// Gets orders by condition.
        /// </summary>
        /// <param name="filter">The filter condition to apply when retrieving orders.</param>
        /// <returns></returns>
        /// <response code="200">Returns the list of orders matching the condition</response>
        /// <response code="500">If there is an internal server error</response>
        /// <response code="400">If the request is invalid</response>
        [HttpGet("search")]
        public async Task<IActionResult> GetByCondition([FromQuery] string filter)
        {
            var orders = await _ordersService.GetOrdersByCondition(filter);
            return Ok(orders);
        }

        /// <summary>
        /// Gets a single order by id.
        /// </summary>
        /// <param name="id">The id of the order to retrieve.</param>
        /// <returns></returns>
        /// <response code="200">Returns the order with the specified id</response>
        /// <response code="404">If the order with the specified id is not found</response>
        /// <response code="500">If there is an internal server error</response>
        /// <response code="400">If the request is invalid</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(o => o.OrderId, id);
            var order = await _ordersService.GetOrderByCondition(filter);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        /// <summary>
        /// Creates a new order.
        /// </summary>
        /// <param name="orderAddRequest">The order data to create.</param>
        /// <returns></returns>
        /// <response code="201">Returns the created order</response>
        /// <response code="500">If there is an internal server error</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderAddRequest orderAddRequest)
        {
            var createdOrder = await _ordersService.AddOrder(orderAddRequest);
            if (createdOrder == null)
            {
                return BadRequest("Failed to create order.");
            }
            return CreatedAtAction(
                nameof(GetById),
                new { id = createdOrder.OrderID },
                createdOrder
            );
        }

        /// <summary>
        /// Updates an existing order.
        /// </summary>
        /// <param name="orderUpdateRequest">The order data to update.</param>
        /// <returns></returns>
        /// <response code="200">Returns the updated order</response>
        /// <response code="404">If the order to update is not found</response>
        /// <response code="500">If there is an internal server error</response>
        /// <response code="400">If the request is invalid</response>
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] OrderUpdateRequest orderUpdateRequest)
        {
            var updatedOrder = await _ordersService.UpdateOrder(orderUpdateRequest);
            if (updatedOrder == null)
            {
                return NotFound("Order not found for update.");
            }
            return Ok(updatedOrder);
        }

        /// <summary>
        /// Deletes an order by id.
        /// </summary>
        /// <param name="id">The id of the order to delete.</param>
        /// <returns></returns>
        /// <response code="200">If the order is successfully deleted</response>
        /// <response code="404">If the order to delete is not found</response>
        /// <response code="500">If there is an internal server error</response>
        /// <response code="400">If the request is invalid</response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _ordersService.DeleteOrder(id);
            if (!isDeleted)
            {
                return NotFound("Order not found for deletion.");
            }
            return Ok("Order deleted successfully.");
        }
    }
}
