using AutoMapper;
using Moq;
using projetoFinal.Application.Dto;
using projetoFinal.Application.Services;
using projetoFinal.Core.Entities;
using projetoFinal.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Xunit;

namespace projetoFinal.Tests
{
    public class SalesServiceTests
    {
        private readonly Mock<ISaleRepository> _saleRepositoryMock;
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<ILogger<SaleService>> _loggerMock;
        private readonly SaleService _saleService;

        public SalesServiceTests()
        {
            _saleRepositoryMock = new Mock<ISaleRepository>();
            _clientRepositoryMock = new Mock<IClientRepository>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<SaleService>>();
            _saleService = new SaleService(
                _saleRepositoryMock.Object,
                _clientRepositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task AddSaleAsync_ClientNotFound_ShouldThrowException()
        {
            var salesDto = new SalesDTO { ClientId = Guid.NewGuid() };
            _clientRepositoryMock.Setup(x => x.GetClientById(salesDto.ClientId)).ReturnsAsync((Client)null);

            await Assert.ThrowsAsync<Exception>(() => _saleService.AddSaleAsync(salesDto));
        }

        [Fact]
        public async Task AddSaleAsync_ShouldAddSaleSuccessfully()
        {
            var salesDto = new SalesDTO
            {
                ClientId = Guid.NewGuid(),
                SaleItens = new List<SaleItensDTO> { new SaleItensDTO() }
            };

            var client = new Client { Id = salesDto.ClientId, Name = "Test Client" };
            _clientRepositoryMock.Setup(x => x.GetClientById(salesDto.ClientId)).ReturnsAsync(client);
            _mapperMock.Setup(x => x.Map<Sales>(salesDto)).Returns(new Sales());

            await _saleService.AddSaleAsync(salesDto);

            _saleRepositoryMock.Verify(x => x.AddSaleAsync(It.IsAny<Sales>()), Times.Once);
            _saleRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteSaleAsync_SaleNotFound_ShouldThrowException()
        {
            var saleId = Guid.NewGuid();
            _saleRepositoryMock.Setup(x => x.GetSaleById(saleId)).ReturnsAsync((Sales)null);

            await Assert.ThrowsAsync<Exception>(() => _saleService.DeleteSaleAsync(saleId));
        }

        [Fact]
        public async Task DeleteSaleAsync_ShouldDeleteSaleSuccessfully()
        {
            var saleId = Guid.NewGuid();
            var sale = new Sales { Id = saleId, ClientId = Guid.NewGuid() };
            _saleRepositoryMock.Setup(x => x.GetSaleById(saleId)).ReturnsAsync(sale);
            _clientRepositoryMock.Setup(x => x.ClientHasSale(sale.ClientId)).ReturnsAsync(false);
            _clientRepositoryMock.Setup(x => x.GetClientById(sale.ClientId)).ReturnsAsync(new Client { Id = sale.ClientId });

            await _saleService.DeleteSaleAsync(saleId);

            _saleRepositoryMock.Verify(x => x.DeleteSaleAsync(sale), Times.Once);
            _clientRepositoryMock.Verify(x => x.DeleteClient(It.IsAny<Client>()), Times.Once);
            _saleRepositoryMock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
