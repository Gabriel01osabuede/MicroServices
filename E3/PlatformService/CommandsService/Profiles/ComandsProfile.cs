using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using PlatformService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommandsService.Profiles
{
    public class ComandsProfile : Profile
    {
        public ComandsProfile()
        {
            //source-Target
            CreateMap<platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<PlatformPublishedDto, platform>()
                .ForMember(destination => destination.ExternalID,
                opt => opt.MapFrom(src => src.Id));
            CreateMap<GrpcPlatformModel, platform>()
                .ForMember(destination => destination.ExternalID, opt => opt.MapFrom(src => src.PlatformId))
                .ForMember(destination => destination.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(destination => destination.Commands, opt => opt.Ignore());
        }

    }

}

