using Entities;
using System.Collections.Generic;

namespace DALayer.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);

        Order GetById(int orderId);

        void Update(Order order);

        IEnumerable<Order> GetActiveOrders();
    }
}
