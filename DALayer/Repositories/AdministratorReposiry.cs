using DALayer.Interfaces;
using Entities;
using System;
using System.Security.Cryptography;
using System.Linq;
using System.Data;
using System.Text;

namespace DALayer.Repositories
{
    public class AdministratorReposiry : IAdministratorRepository
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

            var admin = (from a in context.Administrator
                         where a.Login == userName
                         select a).FirstOrDefault();
            if (admin != null)
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
    }
}
