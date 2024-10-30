using AutoMapper;
using projetoFinal.Application.Dto;
using projetoFinal.Core.Entities;

namespace projetoFinal.Presentation.Mapping
{
    public class SaleMapper : Profile
    {
        public SaleMapper()
        {
            CreateMap<Sales, SalesDTO>()
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.Client.Name));

            CreateMap<SalesDTO, Sales>()
                .ForMember(dest => dest.Client, opt => opt.Ignore())
                .ForMember(dest => dest.ClientName, opt => opt.MapFrom(src => src.ClientName));

            CreateMap<SaleIten, SaleItensDTO>();
            CreateMap<SaleItensDTO, SaleIten>()
                .ForMember(dest => dest.SaleId, opt => opt.Ignore());

        }
    }
}
