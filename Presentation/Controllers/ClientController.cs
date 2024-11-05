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
            private readonly ILogger<ClientController> _logger;

            public ClientController(IClientService clientService, ILogger<ClientController> logger)
            {
                _clientService = clientService;
                _logger = logger;
            }

            [HttpGet]
            public async Task<ActionResult<IEnumerable<Client>>> GetClients()
            {
                try
                {
                    _logger.LogInformation("Iniciando a recuperação de clientes.");
                    var clients = await _clientService.GetAllClientsAsync();
                    return Ok(clients);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao recuperar clientes.");
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet("${id}")]
            public async Task<ActionResult<Client>> GetClientById(Guid id)
            {
                try
                {
                    var client = await _clientService.GetClientById(id);

                    if (client == null)
                    {
                        throw new Exception("Cliente com id não foi encontrado!");
                    }

                    return Ok(client);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao recuperar cliente.");
                    return BadRequest(ex.Message);
                }
            }

        [HttpPost]
            public async Task<ActionResult> AddClientsAsync(ClientDto clientDto)
            {
                if (clientDto == null)
                {
                    _logger.LogWarning("Os dados do cliente não podem ser vazios!");
                    throw new Exception("Os dados do cliente não podem ser vazios!");
                }

                try
                {
                    await _clientService.AddClientsAsync(clientDto);
                    _logger.LogInformation("Cliente cadastrado com sucesso: {Cpf}", clientDto.Cpf);
                    return Ok("Cliente cadastrado com sucesso!");
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Cpf já cadastrado!"))
                    {
                        _logger.LogError(ex, "Erro ao cadastrar cliente.");
                        return Conflict("Esse cpf já está cadastrado!");
                    }

                    return BadRequest(ex.Message);
                }
            }

            [HttpPut("${id}")]
            public async Task<ActionResult> EditClient(ClientDto clientDto, Guid id)
            {
                if (clientDto == null)
                {
                    _logger.LogWarning("Os dados do cliente não podem ser vazios!");
                    throw new Exception("Os dados do cliente não podem ser vazios!");
                }

                try
                {
                    var client = await _clientService.UpdateClientAsync(clientDto, id);
                    return Ok("Cliente foi editado com sucesso!");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao atualizar cliente.");
                    return BadRequest(ex.Message);
                }
            }

            [HttpDelete("${id}")]
            public async Task<ActionResult> DeleteClient(Guid id)
            {
                try
                {
                    var client = _clientService.GetClientById(id);

                    if (client == null)
                    {
                        throw new Exception("Cliente não foi encontrado!");
                    }

                    bool clientHasSale = await _clientService.DeleteClientAsync(id);
                    if (clientHasSale)
                    {
                        throw new Exception("O cliente não pode ser excluido pois possui vendas associadas");
                    }

                    await _clientService.DeleteClientAsync(id);
                    return Ok("Cliente foi excluido com sucesso!");

                }
                catch(Exception ex) 
                {
                    _logger.LogError(ex, "Erro ao excluir cliente.");
                    return BadRequest(ex.Message);
                }
            }
        }
}