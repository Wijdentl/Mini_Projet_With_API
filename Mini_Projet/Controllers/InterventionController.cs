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
    public class InterventionController : ControllerBase
    {
        private readonly IInterventionRepository interventionRepository;

        public InterventionController(IInterventionRepository interventionRepository)
        {
            this.interventionRepository = interventionRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetInterventions()
        {
            try
            {
                return Ok(await interventionRepository.GetInterventions());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Intervention>> GetIntervention(int id)
        {
            try
            {
                var result = await interventionRepository.GetInterventionById(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Intervention>> CreateIntervention(Intervention intervention)
        {
            try
            {
                if (intervention == null)
                {
                    return BadRequest();
                }

                var createdIntervention = await interventionRepository.AddIntervention(intervention);
                return CreatedAtAction(nameof(GetIntervention), new { id = createdIntervention.Id }, createdIntervention);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving data to the database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Intervention>> UpdateIntervention(int id, Intervention intervention)
        {
            try
            {
                if (id != intervention.Id)
                    return BadRequest();

                var interventionToUpdate = await interventionRepository.GetInterventionById(id);
                if (interventionToUpdate == null)
                    return NotFound();

                return await interventionRepository.UpdateIntervention(intervention);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Intervention>> DeleteIntervention(int id)
        {
            try
            {
                var interventionToDelete = await interventionRepository.GetInterventionById(id);
                if (interventionToDelete == null)
                {
                    return NotFound();
                }
                return await interventionRepository.DeleteIntervention(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
