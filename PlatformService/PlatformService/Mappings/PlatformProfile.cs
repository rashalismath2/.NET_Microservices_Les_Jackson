using AutoMapper;
using PlatformService.Dto;
using PlatformService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlatformService.Mappings
{
	public class PlatformProfile : Profile
	{
		public PlatformProfile()
		{
			CreateMap<PlatformCreateDto, Platform>().ReverseMap();
			CreateMap<PlatformReadDto, Platform>().ReverseMap();
		}
	}
}
