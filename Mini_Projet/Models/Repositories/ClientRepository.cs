
using Microsoft.EntityFrameworkCore;
using Mini_Projet.Context;
using Mini_Projet.Models.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Mini_Projet.Models.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext appDbContext;
        public ClientRepository(AppDbContext myappDbContext)
        {
            this.appDbContext = myappDbContext;
        }
        public async Task<IEnumerable<Client>> GetClients()
        {
            return await appDbContext.Clients.ToListAsync();
        }

        // Get client by Id
        public async Task<Client> GetClientById(int clientId)
        {
            return await appDbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);
        }

        // Add a new client
        public async Task<Client> AddClient(Client client)
        {
            appDbContext.Clients.Add(client);
            await appDbContext.SaveChangesAsync();
            return client;
        }

        // Update an existing client
        public async Task<Client> UpdateClient(Client client)
        {
            appDbContext.Clients.Update(client);
            await appDbContext.SaveChangesAsync();
            return client;
        }

        // Delete a client by Id
        public async Task<Client> DeleteClient(int clientId)
        {
            var client = await appDbContext.Clients.FirstOrDefaultAsync(c => c.Id == clientId);
            if (client == null) return null;

            appDbContext.Clients.Remove(client);
            await appDbContext.SaveChangesAsync();
            return client;
        }

        // Get client by Email
        public async Task<Client> GetClientByEmail(string email)
        {
            return await appDbContext.Clients.FirstOrDefaultAsync(c => c.Email == email);
        }

    }
}




