using Mini_Projet.Models.Repositories;
using Mini_Projet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Mini_Projet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReclamationController : ControllerBase
    {
        private readonly IReclamationRepository reclamationRepository;

        public ReclamationController(IReclamationRepository reclamationRepository)
        {
            this.reclamationRepository = reclamationRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetReclamations()
        {
            try
            {
                return Ok(await reclamationRepository.GetReclamations());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Reclamation>> GetReclamation(int id)
        {
            try
            {
                var result = await reclamationRepository.GetReclamationById(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Reclamation>> CreateReclamation(Reclamation reclamation)
        {
            try
            {
                if (reclamation == null)
                {
                    return BadRequest();
                }

                var createdReclamation = await reclamationRepository.AddReclamation(reclamation);
                return CreatedAtAction(nameof(GetReclamation), new { id = createdReclamation.Id }, createdReclamation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving data to the database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Reclamation>> UpdateReclamation(int id, Reclamation reclamation)
        {
            try
            {
                if (id != reclamation.Id)
                    return BadRequest();

                var reclamationToUpdate = await reclamationRepository.GetReclamationById(id);
                if (reclamationToUpdate == null)
                    return NotFound();

                return await reclamationRepository.UpdateReclamation(reclamation);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Reclamation>> DeleteReclamation(int id)
        {
            try
            {
                var reclamationToDelete = await reclamationRepository.GetReclamationById(id);
                if (reclamationToDelete == null)
                {
                    return NotFound();
                }
                return await reclamationRepository.DeleteReclamation(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
