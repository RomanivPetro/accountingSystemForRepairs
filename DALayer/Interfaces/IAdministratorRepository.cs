using Entities;

namespace DALayer.Interfaces
{
    public interface IAdministratorRepository
    {
        bool Login(string userName, string password);

        void Add(Administrator admin);
    }
}
