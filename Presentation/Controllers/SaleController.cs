using Microsoft.AspNetCore.Mvc;
using projetoFinal.Application.Dto;
using projetoFinal.Application.Interfaces;

namespace projetoFinal.Presentation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly ILogger<SaleController> _logger;

        public SaleController(ISaleService saleService, ILogger<SaleController> logger)
        {
            _saleService = saleService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SalesDTO>>> GetAllSales()
        {
            try
            {
                var sales = await _saleService.GetAllSalesAsync();
                return Ok(sales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter todas as vendas.");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesDTO>> GetById(Guid id)
        {
            try
            {
                var sale = await _saleService.GetSaleById(id);
                return Ok(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter a venda com ID {SaleId}.", id);
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> AddSale(SalesDTO salesDto)
        {
            try
            {
                await _saleService.AddSaleAsync(salesDto);
                return Ok("Venda cadastrada com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao cadastrar venda.");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSale(SaleUpdateDTO salesUpdateDto, Guid id)
        {
            try
            {
                await _saleService.UpdateSaleAsync(salesUpdateDto, id);
                return Ok("Venda atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar a venda com ID {SaleId}.", id);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSale(Guid id)
        {
            try
            {
                await _saleService.DeleteSaleAsync(id);
                return Ok("Venda deletada com sucesso!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar a venda com ID {SaleId}.", id);
                return BadRequest(ex.Message);
            }
        }
    }
}
