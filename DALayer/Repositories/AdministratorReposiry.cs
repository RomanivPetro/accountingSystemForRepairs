using DALayer.Interfaces;
using Entities;
using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Threading.Tasks;
using System.Text;

namespace DALayer.Repositories
{
    public class AdministratorReposiry : IAdministratorRepository, IDisposable
    {
        private AccountingContext context;

        public AdministratorReposiry(AccountingContext context)
        {
            this.context = context;
        }

        public bool Login(string userName, string password)
        {
            if(string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Arguments cannot be null or empty");
            }
            password = HashPassword(password);

            var admin = context.Administrator.Where(a => a.Login == userName).FirstOrDefault();
            if(admin != null)
            {
                return password == admin.Password;
            }
            return false;
        }

        private string HashPassword(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(Encoding.Unicode.GetBytes(password));

            byte[] result = md5.Hash;
            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                strBuilder.Append(result[i].ToString("x2"));
            }
            return strBuilder.ToString();
        }

        public void Add(Administrator admin)
        {
            if (admin == null)
            {
                throw new ArgumentNullException("admin cannot be null");
            }
            if (string.IsNullOrEmpty(admin.Login) || string.IsNullOrEmpty(admin.Password))
            {
                throw new ArgumentException("Login and Password cannot be null or empty");
            }
            admin.Password = HashPassword(admin.Password);
            context.Administrator.Add(admin);
            context.SaveChanges();
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
        // ~AdministratorReposiry() {
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
