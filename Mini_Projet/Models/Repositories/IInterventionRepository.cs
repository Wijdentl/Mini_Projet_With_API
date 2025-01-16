namespace Mini_Projet.Models.Repositories
{
    public interface IInterventionRepository
    {
        Task<IEnumerable<Intervention>> GetInterventions();
        Task<Intervention> GetInterventionById(int interventionId);
        Task<Intervention> AddIntervention(Intervention intervention);
        Task<Intervention> UpdateIntervention(Intervention intervention);
        Task<Intervention> DeleteIntervention(int interventionId);
        Task<IEnumerable<Intervention>> GetInterventionsByReclamationIdA(int reclamationId);
    }
}
