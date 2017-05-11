using DALayer.Interfaces;
using DALayer.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DALayer
{
    public class UnitOfWork : IDisposable
    {
        private AccountingContext context = new AccountingContext();
        private AdministratorReposiry adminRepository;
        private OrderRepository orderRepository;
        private WorkerRepository workerRepository;

        public IAdministratorRepository AdministratorRepository
        {
            get
            {
                if (adminRepository == null)
                {
                    adminRepository = new AdministratorReposiry(context);
                }
                return adminRepository;
            }
        }

        public IOrderRepository OrderRepository
        {
            get
            {
                if(orderRepository == null)
                {
                    orderRepository = new OrderRepository(context);
                }
                return orderRepository;
            }
        }

        public IWorkerRepository WorkerRepository
        {
            get
            {
                if (workerRepository == null)
                {
                    workerRepository = new WorkerRepository(context);
                }
                return workerRepository;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
