using DALayer.Interfaces;
using System;
using Entities;

namespace DALayer.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private AccountingContext context;

        public OrderRepository(AccountingContext context)
        {
            this.context = context;
        }

        private void Save()
        {
            context.SaveChanges();
        }

        public void AddOrder(Order order)
        {
            if(order == null)
            {
                throw new ArgumentNullException("Order cannot be null");
            }
            context.Order.Add(order);
            Save();
        }

        public Order GetById(int orderId)
        {
            if(orderId < 1)
            {
                throw new ArgumentException("OrderId is invalid");
            }
            var order = context.Order.Find(orderId);
            return order;
        }

        public void Update(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException("Order cannot be null");
            }
            context.Entry(order).State = System.Data.Entity.EntityState.Modified;
            Save();
        }
    }
}
