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

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
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
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SalesDTO>> GetById(Guid id)
        {
            var sale = await _saleService.GetSaleById(id);

            if (sale == null)
            {
                return NotFound("Venda não foi encontrada!");
            }

            return Ok(sale);

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
                return BadRequest(ex.Message);
            }
        }
    }
}
