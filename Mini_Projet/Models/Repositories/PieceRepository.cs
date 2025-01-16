using Microsoft.EntityFrameworkCore;
using Mini_Projet.Context;

namespace Mini_Projet.Models.Repositories
{
    public class PieceRepository : IPieceRepository
    {
        private readonly AppDbContext appDbContext;
        public PieceRepository(AppDbContext myappDbContext)
        {
            this.appDbContext = myappDbContext;
        }
        // Get all pieces de rechange
        public async Task<IEnumerable<PieceDeRechange>> GetPiecesDeRechange()
        {
            return await appDbContext.PieceDeRechanges
                .Include(p => p.Article) // Eager load the Article if needed
                .ToListAsync();
        }

        // Get piece de rechange by Id
        public async Task<PieceDeRechange> GetPieceDeRechangeById(int pieceId)
        {
            return await appDbContext.PieceDeRechanges
                .Include(p => p.Article) // Eager load the Article if needed
                .FirstOrDefaultAsync(p => p.Id == pieceId);
        }

        // Add a new piece de rechange
        public async Task<PieceDeRechange> AddPieceDeRechange(PieceDeRechange piece)
        {
            appDbContext.PieceDeRechanges.Add(piece);
            await appDbContext.SaveChangesAsync();
            return piece;
        }

        // Update an existing piece de rechange
        public async Task<PieceDeRechange> UpdatePieceDeRechange(PieceDeRechange piece)
        {
            appDbContext.PieceDeRechanges.Update(piece);
            await appDbContext.SaveChangesAsync();
            return piece;
        }

        // Delete a piece de rechange by Id
        public async Task<PieceDeRechange> DeletePieceDeRechange(int pieceId)
        {
            var piece = await appDbContext.PieceDeRechanges
                .FirstOrDefaultAsync(p => p.Id == pieceId);
            if (piece == null) return null;

            appDbContext.PieceDeRechanges.Remove(piece);
            await appDbContext.SaveChangesAsync();
            return piece;
        }

        // Get pieces de rechange by ArticleId
        public async Task<IEnumerable<PieceDeRechange>> GetPiecesDeRechangeByArticleId(int articleId)
        {
            return await appDbContext.PieceDeRechanges
                .Where(p => p.ArticleId == articleId)
                .Include(p => p.Article) // Eager load the Article if needed
                .ToListAsync();
        }
    }
}
