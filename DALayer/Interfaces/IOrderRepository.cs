using Entities;

namespace DALayer.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);

        Order GetById(int orderId);

        void Update(Order order);
    }
}
