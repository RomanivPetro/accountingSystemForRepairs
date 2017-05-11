using Entities;
using System;
using System.Collections.Generic;

namespace DALayer.Interfaces
{
    public interface IWorkerRepository
    {
        void Add(Worker worker);

        void Delete(Worker worker);

        IEnumerable<Worker> GetWorkers();

        int ActiveOrdersCount(Worker worker, DateTime fromDate, DateTime toDate);

        int DoneOrdersCount(Worker worker, DateTime fromDate, DateTime toDate);

        decimal GetDoneOrdersIncome(Worker worker, DateTime fromDate, DateTime toDate);
    }
}
