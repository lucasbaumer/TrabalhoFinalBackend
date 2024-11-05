using AutoMapper;
using Microsoft.Extensions.Logging;
using projetoFinal.Application.Dto;
using projetoFinal.Application.Interfaces;
using projetoFinal.Core.Entities;
using projetoFinal.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetoFinal.Application.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SaleService> _logger;

        public SaleService(ISaleRepository saleRepository, IClientRepository clientRepository, IMapper mapper, ILogger<SaleService> logger)
        {
            _saleRepository = saleRepository;
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddSaleAsync(SalesDTO salesDto)
        {
            var client = await _clientRepository.GetClientById(salesDto.ClientId);

            if(client == null)
            {
                _logger.LogWarning("O cliente com ID {ClientId} não foi encontrado.", salesDto.ClientId);
                throw new Exception("O cliente não foi encontrado!");
            }
            
            salesDto.ClientName = client.Name;
            var sale = _mapper.Map<Sales>(salesDto);

            await _saleRepository.AddSaleAsync(sale);
            await _saleRepository.SaveChangesAsync();
            _logger.LogInformation("Venda adicionada com sucesso para o cliente {ClientName}.", client.Name);
        }

        public async Task DeleteSaleAsync(Guid id)
        {
            var sale = await _saleRepository.GetSaleById(id);

            if(sale == null)
            {
                _logger.LogWarning("A venda com ID {SaleId} não foi encontrada.", id);
                throw new Exception("A venda não foi encontrada!");
            }

            var clientId = sale.ClientId;
            _saleRepository.DeleteSaleAsync(sale);
            await _saleRepository.SaveChangesAsync();

            var client = await _clientRepository.GetClientById(id);

            if (clientId != null)
            {
                var hasOtherSales = await _clientRepository.ClientHasSale(clientId);
                if (!hasOtherSales)
                {
                    _clientRepository.DeleteClient(client);
                    await _clientRepository.SaveChangesAsync();
                    _logger.LogInformation("Cliente {ClientId} excluído porque não possui mais vendas.", clientId);
                }
            }

            _logger.LogInformation("Venda com ID {SaleId} deletada com sucesso.", id);
        }

        public async Task<IEnumerable<Sales>> GetAllSalesAsync()
        {
            var sales = await _saleRepository.GetAllSalesAsync();

            if (sales == null)
            {
                _logger.LogWarning("Nenhuma venda foi encontrada.");
                throw new Exception("Nenhum venda foi encontrada!");
            }

            return sales;  
        }

        public async Task<SalesDTO> GetSaleById(Guid id)
        {
            var sale = await _saleRepository.GetSaleById(id);

            if (sale == null)
            {
                _logger.LogWarning("Venda com ID {SaleId} não foi encontrada.", id);
                throw new Exception("Venda não foi encontrada!");
            }
            return _mapper.Map<SalesDTO>(sale);
        }

        public async Task UpdateSaleAsync(SaleUpdateDTO salesUpdateDto, Guid id)
        {
            var sale = await _saleRepository.GetSaleById(id);

            if(sale == null)
            {
                _logger.LogWarning("Venda com ID {SaleId} não foi encontrada.", id);
                throw new Exception("Venda não foi encontrada!");
            }

            _mapper.Map(salesUpdateDto.SaleItens, sale.SaleItens);

            _saleRepository.UpdateSale(sale);
            _saleRepository.SaveChangesAsync();
            _logger.LogInformation("Venda com ID {SaleId} atualizada com sucesso.", id);
        }
    }
}
