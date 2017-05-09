using Entities;

namespace DALayer.Interfaces
{
    public interface IAdministratorRepository
    {
        /// <summary>
        /// Indicates is administrator can login.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Login(string userName, string password);

        /// <summary>
        /// Adds new Administrator to DB.
        /// </summary>
        /// <param name="admin"></param>
        void Add(Administrator admin);
    }
}
