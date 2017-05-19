using Entities;
using System;

namespace DALayer.Interfaces
{
    public interface ISpendingRepository
    {
        void Add(Spending spending);

        decimal GetSpendingsCost(DateTime fromDate, DateTime toDate);
    }
}
