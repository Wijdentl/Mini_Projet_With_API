using Mini_Projet.Models.Repositories;
using Mini_Projet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Mini_Projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PieceDeRechangeController : ControllerBase
    {
        private readonly IPieceRepository pieceRepository;

        public PieceDeRechangeController(IPieceRepository pieceRepository)
        {
            this.pieceRepository = pieceRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetPieces()
        {
            try
            {
                return Ok(await pieceRepository.GetPiecesDeRechange());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PieceDeRechange>> GetPiece(int id)
        {
            try
            {
                var result = await pieceRepository.GetPieceDeRechangeById(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<PieceDeRechange>> CreatePiece(PieceDeRechange piece)
        {
            try
            {
                if (piece == null)
                {
                    return BadRequest();
                }

                var createdPiece = await pieceRepository.AddPieceDeRechange(piece);
                return CreatedAtAction(nameof(GetPiece), new { id = createdPiece.Id }, createdPiece);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving data to the database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<PieceDeRechange>> UpdatePiece(int id, PieceDeRechange piece)
        {
            try
            {
                if (id != piece.Id)
                    return BadRequest();

                var pieceToUpdate = await pieceRepository.GetPieceDeRechangeById(id);
                if (pieceToUpdate == null)
                    return NotFound();

                return await pieceRepository.UpdatePieceDeRechange(piece);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PieceDeRechange>> DeletePiece(int id)
        {
            try
            {
                var pieceToDelete = await pieceRepository.GetPieceDeRechangeById(id);
                if (pieceToDelete == null)
                {
                    return NotFound();
                }
                return await pieceRepository.DeletePieceDeRechange(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
