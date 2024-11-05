using AutoMapper;
using Moq;
using projetoFinal.Application.Dto;
using projetoFinal.Application.Interfaces;
using projetoFinal.Application.Services;
using projetoFinal.Core.Entities;
using projetoFinal.Core.Interfaces;
using Xunit;

namespace projetoFinal.Tests
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly IClientService _clientService;
        private readonly Mock<IMapper> _mapperMock;

        public ClientServiceTests()
        {
            _clientRepositoryMock = new Mock<IClientRepository>();
            _mapperMock = new Mock<IMapper>();
            _clientService = new ClientService(_mapperMock.Object, _clientRepositoryMock.Object);
        }

        [Fact]
        public async Task AddClientsAsync_ShouldAddClient_WhenClientIsValid()
        {
            var clientDto = new ClientDto
            {
                Name = "John Doe",
                Cpf = "12345678900",
                Email = "john.doe@example.com",
                Telefone = "123456789"
            };

            var client = new Client
            {
                Id = Guid.NewGuid(),
                Name = clientDto.Name,
                Cpf = clientDto.Cpf,
                Email = clientDto.Email,
                Telefone = clientDto.Telefone
            };

            _mapperMock.Setup(m => m.Map<Client>(clientDto)).Returns(client);

            _clientRepositoryMock.Setup(r => r.GetClientByCpf(client.Cpf)).ReturnsAsync((Client)null);

            await _clientService.AddClientsAsync(clientDto);

            _clientRepositoryMock.Verify(r => r.AddClientAsync(It.IsAny<Client>()), Times.Once);
            _clientRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteClientAsync_ShouldReturnTrue_WhenClientHasSales()
        {
            var clientId = Guid.NewGuid();
            var client = new Client { Id = clientId };

            _clientRepositoryMock.Setup(r => r.GetClientById(clientId)).ReturnsAsync(client);
            _clientRepositoryMock.Setup(r => r.ClientHasSale(clientId)).ReturnsAsync(true);

            var result = await _clientService.DeleteClientAsync(clientId);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateClientAsync_ShouldReturnFalse_WhenClientDoesNotExist()
        {
            var clientDto = new ClientDto { Name = "Jane Doe", Cpf = "12345678901" };
            var clientId = Guid.NewGuid();

            _clientRepositoryMock.Setup(r => r.GetClientById(clientId)).ReturnsAsync((Client)null);

            var result = await _clientService.UpdateClientAsync(clientDto, clientId);

            Assert.False(result);
        }
    }
}