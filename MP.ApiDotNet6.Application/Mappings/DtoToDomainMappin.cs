using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ApiDotNet6.Application.Mappings;

public class DtoToDomainMappin : Profile
{
	public DtoToDomainMappin()
	{
		CreateMap<PersonDTO, Person>();
		CreateMap<ProductDTO, Product>();
	}
}
