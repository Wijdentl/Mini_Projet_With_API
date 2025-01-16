using Mini_Projet.Models.Repositories;
using Mini_Projet.Models;
using Microsoft.EntityFrameworkCore;
using Mini_Projet.Context;
namespace Mini_Projet.Models.Repositories
{
    public class ReclamationRepository : IReclamationRepository
    {
        private readonly AppDbContext appDbContext;
        public ReclamationRepository(AppDbContext myappDbContext)
        {
            this.appDbContext = myappDbContext;
        }
        // Get all reclamations
        public async Task<IEnumerable<Reclamation>> GetReclamations()
        {
            return await appDbContext.Reclamations
                .Include(r => r.Client) // Eager load the Client if needed
                .ToListAsync();
        }

        // Get reclamation by Id
        public async Task<Reclamation> GetReclamationById(int reclamationId)
        {
            return await appDbContext.Reclamations
                .Include(r => r.Client) // Eager load the Client if needed
                .FirstOrDefaultAsync(r => r.Id == reclamationId);
        }

        // Add a new reclamation
        public async Task<Reclamation> AddReclamation(Reclamation reclamation)
        {
            appDbContext.Reclamations.Add(reclamation);
            await appDbContext.SaveChangesAsync();
            return reclamation;
        }

        // Update an existing reclamation
        public async Task<Reclamation> UpdateReclamation(Reclamation reclamation)
        {
            appDbContext.Reclamations.Update(reclamation);
            await appDbContext.SaveChangesAsync();
            return reclamation;
        }

        // Delete a reclamation by Id
        public async Task<Reclamation> DeleteReclamation(int reclamationId)
        {
            var reclamation = await appDbContext.Reclamations
                .FirstOrDefaultAsync(r => r.Id == reclamationId);

            if (reclamation == null) return null;

            appDbContext.Reclamations.Remove(reclamation);
            await appDbContext.SaveChangesAsync();
            return reclamation;
        }

        // Get reclamations by ClientId
        public async Task<IEnumerable<Reclamation>> GetReclamationsByClientId(int clientId)
        {
            return await appDbContext.Reclamations
                .Where(r => r.ClientId == clientId)
                .Include(r => r.Client) // Eager load the Client if needed
                .ToListAsync();
        }
    }
}


