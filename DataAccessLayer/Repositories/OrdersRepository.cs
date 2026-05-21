using DataAccessLayer.Entities;
using DataAccessLayer.RepositoryContracts;
using MongoDB.Driver;

namespace DataAccessLayer.Repositories
{
    public class OrdersRepository : IOrdersRepository
    {
        private readonly IMongoCollection<Order> _orders;
        private readonly string collectionName = "orders";

        public OrdersRepository(IMongoDatabase mongoDatabase)
        {
            _orders = mongoDatabase.GetCollection<Order>(collectionName);
        }

        public async Task<Order?> AddOrder(Order order)
        {
            order.OrderId = Guid.NewGuid();
            order._id = order.OrderId;
            foreach (OrderItem item in order.Items)
            {
                item._id = Guid.NewGuid();
            }
            await _orders.InsertOneAsync(order);
            return order;
        }

        public async Task<bool> DeleteOrder(Guid orderID)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(
                temp => temp.OrderId,
                orderID
            );

            Order? existingOrder = (await _orders.FindAsync(filter)).FirstOrDefault();

            if (existingOrder == null)
            {
                return false;
            }

            DeleteResult deleteResult = await _orders.DeleteOneAsync(filter);

            return deleteResult.DeletedCount > 0;
        }

        public async Task<Order?> GetOrderByCondition(FilterDefinition<Order> filter)
        {
            return (await _orders.FindAsync(filter)).FirstOrDefault();
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return (await _orders.FindAsync(Builders<Order>.Filter.Empty)).ToList();
        }

        public Task<IEnumerable<Order>> GetOrders(Order order)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Order?>> GetOrdersByCondition(FilterDefinition<Order> filter)
        {
            return (await _orders.FindAsync(filter)).ToList();
        }

        public async Task<Order?> UpdateOrder(Order order)
        {
            FilterDefinition<Order> filter = Builders<Order>.Filter.Eq(
                temp => temp.OrderId,
                order.OrderId
            );

            Order? existingOrder = (await _orders.FindAsync(filter)).FirstOrDefault();

            if (existingOrder == null)
            {
                return null;
            }

            ReplaceOneResult replaceOneResult = await _orders.ReplaceOneAsync(filter, order);

            return order;
        }
    }
}
