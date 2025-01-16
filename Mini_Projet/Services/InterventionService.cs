using Mini_Projet.Models.Repositories;

namespace Mini_Projet.Services
{
    public class InterventionService
    {
        private readonly IInterventionRepository _interventionRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IPieceRepository _pieceRepository;

        public InterventionService(
            IInterventionRepository interventionRepository,
            IArticleRepository articleRepository,
            IPieceRepository pieceRepository)
        {
            _interventionRepository = interventionRepository;
            _articleRepository = articleRepository;
            _pieceRepository = pieceRepository;
        }

        // Method to calculate the invoice for the intervention
        public async Task<decimal> CalculateInvoiceAsync(int interventionId)
        {
            // Retrieve the intervention details
            var intervention = await _interventionRepository.GetInterventionById(interventionId);
            if (intervention == null)
            {
                throw new Exception("Intervention not found");
            }

            // Retrieve the article associated with the intervention
            var article = await _articleRepository.GetArticleById(intervention.Reclamation.ArticleId);
            if (article == null)
            {
                throw new Exception("Article not found");
            }

            // Check if the article is still under warranty
            var isUnderWarranty = IsUnderWarranty(article.Garantie, intervention.Date);

            if (isUnderWarranty)
            {
                return 0m; // Free intervention if under warranty
            }
            else
            {
                // Calculate the total cost of pieces used in the intervention
                var partsCost = await CalculatePartsCostAsync(article.Id);

                // Calculate labor cost (fixed cost for simplicity, can be dynamic)
                var laborCost = 50m; // Example labor cost (you can make this dynamic based on the intervention type)

                // Return total invoice cost (parts cost + labor cost)
                return partsCost + laborCost;
            }
        }

        // Helper method to check if the article is under warranty
        private bool IsUnderWarranty(int warrantyMonths, DateTime interventionDate)
        {
            var warrantyExpiryDate = interventionDate.AddMonths(-warrantyMonths);
            return interventionDate <= warrantyExpiryDate;
        }

        // Helper method to calculate the cost of pieces used in the intervention
        private async Task<decimal> CalculatePartsCostAsync(int articleId)
        {
            var pieces = await _pieceRepository.GetPiecesDeRechangeByArticleId(articleId);
            decimal totalPartsCost = 0m;

            foreach (var piece in pieces)
            {
                totalPartsCost += piece.Prix;
            }

            return totalPartsCost;
        }
    }

}
