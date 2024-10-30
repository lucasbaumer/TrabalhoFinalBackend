using Microsoft.AspNetCore.Mvc;
using projetoFinal.Application.Dto;
using projetoFinal.Application.Interfaces;
using projetoFinal.Core.Entities;

namespace projetoFinal.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            try
            {
               var clients = await _clientService.GetAllClientsAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("${id}")]
        public async Task<ActionResult<Client>> GetClientById(Guid id)
        {
            var client = await _clientService.GetClientById(id);

            if(client == null)
            {
                throw new Exception("Cliente com id não foi encontrado!");
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult> AddClientsAsync(ClientDto clientDto)
        {
            if(clientDto == null)
            {
                throw new Exception("Os dados do cliente não podem ser vazios!");
            }

            try
            {
                await _clientService.AddClientsAsync(clientDto);
                return Ok("Cliente cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                if(ex.Message.Contains("Cpf já cadastrado!"))
                {
                    return Conflict("Esse cpf já está cadastrado!");
                }

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("${id}")]
        public async Task<ActionResult> EditClient(ClientDto clientDto, Guid id)
        {
           if(clientDto == null)
            {
                throw new Exception("Os dados do cliente não podem ser vazios!");
            }

            try
            {
                var client = await _clientService.UpdateClientAsync(clientDto, id);
                return Ok("Cliente foi editado com sucesso!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("${id}")]
        public async Task<ActionResult> DeleteClient(Guid id)
        {
            var client = _clientService.GetClientById(id);

            if(client == null)
            {
                throw new Exception("Cliente não foi encontrado!");
            }

            bool clientHasSale = await _clientService.DeleteClientAsync(id);
            if(clientHasSale)
            {
                throw new Exception("O cliente não pode ser excluido pois possui vendas associadas");
            }

            await _clientService.DeleteClientAsync(id);
            return Ok("Cliente foi excluido com sucesso!");
        }
    }
}
