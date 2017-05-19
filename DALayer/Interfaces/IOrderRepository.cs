using Entities;
using System;
using System.Collections.Generic;

namespace DALayer.Interfaces
{
    public interface IOrderRepository
    {
        void AddOrder(Order order);

        Order GetById(int orderId);

        void Update(Order order);

        IEnumerable<Order> GetActiveOrders();

        IEnumerable<Order> FindByPhone(string phoneNumber);

        decimal GetDoneOrdersCost(DateTime fromDate, DateTime toDate);

        decimal GetDoneOrdersIncome(DateTime fromDate, DateTime toDate);

        int GetOrdersCount(DateTime fromDate, DateTime toDate);

        int GetDoneOrdersCount(DateTime fromDate, DateTime toDate);
    }
}
