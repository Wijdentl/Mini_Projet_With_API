namespace Mini_Projet.Models
{
    public class Reclamation
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }
    }
}
