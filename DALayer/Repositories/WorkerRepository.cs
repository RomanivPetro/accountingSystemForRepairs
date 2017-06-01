using DALayer.Interfaces;
using System;
using System.Collections.Generic;
using Entities;
using System.Linq;

namespace DALayer.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        private AccountingContext context;

        public WorkerRepository(AccountingContext context)
        {
            this.context = context;
        }
        // remove this method. Use unitOfWork.Save()
        private void Save()
        {
            context.SaveChanges();
        }

        private IQueryable<Order> GetOrdersQuery(Worker worker, DateTime fromDate, DateTime toDate)
        {
            if (worker == null)
            {
                throw new ArgumentNullException("Worker cannot be null");
            }
            var query = from w in context.Worker
                        where w.Id == worker.Id
                        from o in w.Order
                        where fromDate <= o.ReceptionDate
                        && o.ReceptionDate <= toDate
                        select o;
            return query;
        }

        //not tested!!!
        public int ActiveOrdersCount(Worker worker, DateTime fromDate, DateTime toDate)
        {
            var query = GetOrdersQuery(worker, fromDate, toDate)
                .Where(o => o.GivingDate == null);
            return query.Count();
        }

        public void Add(Worker worker)
        {
            if (worker == null)
            {
                throw new ArgumentNullException("Worker cannot be null");
            }
            context.Worker.Add(worker);
            Save();
        }

        public void Delete(Worker worker)
        {
            if(worker == null)
            {
                throw new ArgumentNullException("Worker cannot be null");
            }
            foreach (var item in worker.Order)
            {
                item.Worker.Remove(worker);
            }
            context.Worker.Remove(worker);
            Save();
        }

        public void Update(Worker worker)
        {
            if (worker == null)
            {
                throw new ArgumentNullException("Worker cannot be null");
            }
            if (string.IsNullOrWhiteSpace(worker.Name))
            {
                throw new ArgumentException("Worker name is invalid");
            }
            context.Entry(worker).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public int DoneOrdersCount(Worker worker, DateTime fromDate, DateTime toDate)
        {
            var query = GetOrdersQuery(worker, fromDate, toDate)
                .Where(o => o.GivingDate != null);
            return query.Count();
        }

        public IEnumerable<Worker> GetWorkers()
        {
            return context.Worker.ToList();
        }

        public decimal GetDoneOrdersIncome(Worker worker, DateTime fromDate, DateTime toDate)
        {
            var query = GetOrdersQuery(worker, fromDate, toDate)
                .Where(o => o.GivingDate != null)
                .Select(o => o.Income)
                .DefaultIfEmpty(0);
            return query.Sum();
        }
    }
}
