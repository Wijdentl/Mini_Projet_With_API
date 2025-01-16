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
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetClients()
        {
            try
            {
                return Ok(await clientRepository.GetClients());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            try
            {
                var result = await clientRepository.GetClientById(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Client>> CreateClient(Client client)
        {
            try
            {
                if (client == null)
                {
                    return BadRequest();
                }
                else
                {
                    var existingClient = await clientRepository.GetClientByEmail(client.Email);
                    if (existingClient != null)
                    {
                        ModelState.AddModelError("email", "Client email already in use");
                        return BadRequest(ModelState);
                    }
                    else
                    {
                        var createdClient = await clientRepository.AddClient(client);
                        return CreatedAtAction(nameof(GetClient), new { id = createdClient.Id }, createdClient);
                    }
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error saving data to the database");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Client>> UpdateClient(int id, Client client)
        {
            try
            {
                if (id != client.Id)
                    return BadRequest();

                var clientToUpdate = await clientRepository.GetClientById(id);
                if (clientToUpdate == null)
                    return NotFound();

                return await clientRepository.UpdateClient(client);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Client>> DeleteClient(int id)
        {
            try
            {
                var clientToDelete = await clientRepository.GetClientById(id);
                if (clientToDelete == null)
                {
                    return NotFound();
                }
                return await clientRepository.DeleteClient(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
