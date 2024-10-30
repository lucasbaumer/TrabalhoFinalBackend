using AutoMapper;
using projetoFinal.Application.Dto;
using projetoFinal.Core.Entities;

namespace projetoFinal.Presentation.Mapping
{
    public class ClientMapper : Profile
    {
       public ClientMapper()
        {
            CreateMap<ClientDto, Client>();
            CreateMap<Client, ClientDto>();
        }
        
         
    }
}
