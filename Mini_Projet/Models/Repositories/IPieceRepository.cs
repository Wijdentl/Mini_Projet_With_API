namespace Mini_Projet.Models.Repositories
{
    public interface IPieceRepository
    {
        Task<IEnumerable<PieceDeRechange>> GetPiecesDeRechange();
        Task<PieceDeRechange> GetPieceDeRechangeById(int pieceId);
        Task<PieceDeRechange> AddPieceDeRechange(PieceDeRechange piece);
        Task<PieceDeRechange> UpdatePieceDeRechange(PieceDeRechange piece);
        Task<PieceDeRechange> DeletePieceDeRechange(int pieceId);
        Task<IEnumerable<PieceDeRechange>> GetPiecesDeRechangeByArticleId(int articleId);
    }
}
