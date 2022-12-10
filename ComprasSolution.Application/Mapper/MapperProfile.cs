using AutoMapper;
using ComprasSolution.Application.DTOs;
using ComprasSolution.Domain.Entities;

namespace ComprasSolution.Application.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseDetailDTO>()
                .ForMember(x => x.Person, opt => opt.Ignore())
                .ForMember(x => x.Product, opt => opt.Ignore())
                .ConstructUsing((model, context) =>
                {
                    var dto = new PurchaseDetailDTO
                    {
                        Product = model.Product.Name,
                        Id = model.Id,
                        Date = model.Date,
                        Person = model.Person.Name
                    };
                    return dto;
                });
        }
    }
}
