using Mini_Projet.Models.Repositories;
using Mini_Projet.Models;
using Microsoft.EntityFrameworkCore;
using Mini_Projet.Context;


namespace Mini_Projet.Models.Repositories
{
    public class InterventionRepository : IInterventionRepository
    {
        private readonly AppDbContext appDbContext;

        public InterventionRepository(AppDbContext context)
        {
            appDbContext = context;
        }

        // Get all interventions
        public async Task<IEnumerable<Intervention>> GetInterventions()
        {
            return await appDbContext.Interventions
                .Include(i => i.Reclamation) // Eager load Reclamation if needed
                .ToListAsync();
        }

        // Get intervention by Id
        public async Task<Intervention> GetInterventionById(int interventionId)
        {
            return await appDbContext.Interventions
                .Include(i => i.Reclamation) // Eager load Reclamation if needed
                .FirstOrDefaultAsync(i => i.Id == interventionId);
        }

        // Add a new intervention
        public async Task<Intervention> AddIntervention(Intervention intervention)
        {
            appDbContext.Interventions.Add(intervention);
            await appDbContext.SaveChangesAsync();
            return intervention;
        }

        // Update an existing intervention
        public async Task<Intervention> UpdateIntervention(Intervention intervention)
        {
            appDbContext.Interventions.Update(intervention);
            await appDbContext.SaveChangesAsync();
            return intervention;
        }

        // Delete an intervention by Id
        public async Task<Intervention> DeleteIntervention(int interventionId)
        {
            var intervention = await appDbContext.Interventions
                .FirstOrDefaultAsync(i => i.Id == interventionId);

            if (intervention == null) return null;

            appDbContext.Interventions.Remove(intervention);
            await appDbContext.SaveChangesAsync();
            return intervention;
        }

        // Get interventions by Reclamation Id
        public async Task<IEnumerable<Intervention>> GetInterventionsByReclamationIdA(int reclamationId)
        {
            return await appDbContext.Interventions
                .Where(i => i.ReclamationId == reclamationId)
                .Include(i => i.Reclamation) // Eager load Reclamation if needed
                .ToListAsync();
        }
    }
}
