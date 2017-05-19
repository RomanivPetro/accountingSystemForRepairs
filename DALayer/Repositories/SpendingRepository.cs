using DALayer.Interfaces;
using System;
using System.Linq;
using Entities;

namespace DALayer.Repositories
{
    public class SpendingRepository : ISpendingRepository
    {
        private AccountingContext context;

        public SpendingRepository(AccountingContext context)
        {
            this.context = context;
        }

        private void Save()
        {
            context.SaveChanges();
        }

        private IQueryable<Spending> GetSpendingsQuery(DateTime fromDate, DateTime toDate)
        {
            var query = from spending in context.Spending
                        where fromDate <= spending.Date
                        && spending.Date <= toDate
                        select spending;
            return query;
        }

        public void Add(Spending spending)
        {
            if (spending == null)
            {
                throw new ArgumentNullException("Spending cannot be null");
            }
            context.Spending.Add(spending);
            Save();
        }

        public decimal GetSpendingsCost(DateTime fromDate, DateTime toDate)
        {
            var query = GetSpendingsQuery(fromDate, toDate);
            return query.Select(s => s.Cost).AsEnumerable().Sum();
        }
    }
}
