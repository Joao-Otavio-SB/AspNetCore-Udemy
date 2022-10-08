using AutoMapper;
using DotNetRPG.DTOs.Character;
using DotNetRPG.Models;

namespace DotNetRPG
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<CreateCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
        }
    }
}
