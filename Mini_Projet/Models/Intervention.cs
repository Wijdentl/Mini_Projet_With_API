namespace Mini_Projet.Models
{
    public class Intervention
    {
        public int Id { get; set; }
        public int ReclamationId { get; set; }
        public Reclamation Reclamation { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool SousGarantie { get; set; }
        public decimal MontantFacture { get; set; }
    }
}
