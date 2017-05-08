namespace DALayer.Interfaces
{
    public interface IAdministratorRepository
    {
        bool Login(string userName, string password);
    }
}
