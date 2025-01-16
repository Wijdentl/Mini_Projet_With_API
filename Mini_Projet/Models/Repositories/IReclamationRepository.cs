namespace Mini_Projet.Models.Repositories
{
    public interface IReclamationRepository
    {
        Task<IEnumerable<Reclamation>> GetReclamations();
        Task<Reclamation> GetReclamationById(int reclamationId);
        Task<Reclamation> AddReclamation(Reclamation reclamation);
        Task<Reclamation> UpdateReclamation(Reclamation reclamation);
        Task<Reclamation> DeleteReclamation(int reclamationId);
        Task<IEnumerable<Reclamation>> GetReclamationsByClientId(int clientId);
    }
}
