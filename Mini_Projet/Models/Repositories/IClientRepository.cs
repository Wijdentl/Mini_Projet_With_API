namespace Mini_Projet.Models.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetClients();
        Task<Client> GetClientById(int clientId);
        Task<Client> AddClient(Client client);
        Task<Client> UpdateClient(Client client);
        Task<Client> DeleteClient(int clientId);
        Task<Client> GetClientByEmail(string email);
    }
}
