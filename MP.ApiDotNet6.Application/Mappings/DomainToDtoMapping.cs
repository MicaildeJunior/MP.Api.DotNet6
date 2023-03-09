using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ApiDotNet6.Application.Mappings;

public class DomainToDtoMapping : Profile
{
	public DomainToDtoMapping()
	{
		CreateMap<Person, PersonDTO>();
        CreateMap<Product, ProductDTO>();
        // Abaixo precisa ignorar alguns campos no mapeamento, se não vai buscar a Entidade, não a Dto
        CreateMap<Purchase, PurchaseDetailDTO>()
            .ForMember(lbda => lbda.Person, opt => opt.Ignore())
            .ForMember(lbda => lbda.Product, opt => opt.Ignore())
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
