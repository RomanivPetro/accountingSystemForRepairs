using DALayer.Interfaces;
using System;
using Entities;
using System.Linq;
using System.Collections.Generic;

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

        private IQueryable<Order> GetOrdersQuery(DateTime fromDate, DateTime toDate)
        {
            var query = from order in context.Order
                        where fromDate <= order.ReceptionDate
                        && order.ReceptionDate <= toDate
                        orderby order.ReceptionDate
                        select order;
            return query;
        }

        private IQueryable<Order> GetDoneOrdersQuery(DateTime fromDate, DateTime toDate)
        {
            var query = from order in GetOrdersQuery(fromDate, toDate)
                        where order.GivingDate != null
                        select order;
            return query;
        }

        private IQueryable<Order> GetActiveOrdersQuery()
        {
            var query = from order in context.Order
                        where order.GivingDate == null
                        orderby order.ReceptionDate
                        select order;
            return query;
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

        public IEnumerable<Order> FindByPhone(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                throw new ArgumentException("Phone number cannot be empty");
            }
            var query = from order in context.Order
                        where order.PhoneNumber.Contains(phoneNumber)
                        select order;
            return query.AsEnumerable();
        }

        public IEnumerable<Order> GetActiveOrders()
        {
            return GetActiveOrdersQuery().AsEnumerable();
        }

        public decimal GetDoneOrdersCost(DateTime fromDate, DateTime toDate)
        {
            var query = GetDoneOrdersQuery(fromDate, toDate);
            return query.Select(o => o.Cost).AsEnumerable().Sum();
        }

        public decimal GetDoneOrdersIncome(DateTime fromDate, DateTime toDate)
        {
            var query = GetDoneOrdersQuery(fromDate, toDate);
            return query.Select(o => o.Income).AsEnumerable().Sum();
        }

        public int GetOrdersCount(DateTime fromDate, DateTime toDate)
        {
            return GetOrdersQuery(fromDate, toDate).Count();
        }

        public int GetDoneOrdersCount(DateTime fromDate, DateTime toDate)
        {
            return GetDoneOrdersQuery(fromDate, toDate).Count();
        }
    }
}
