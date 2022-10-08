using AutoMapper;
using DotNetRPG.DTOs.Character;
using DotNetRPG.Models;

namespace DotNetRPG.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>()
        {
            new Character(),
            new Character {Id = 1, Name = "PENIS"},
            new Character { Id = 2}
        };

        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data = characters.Select(character => _mapper.Map<GetCharacterDto>(character)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var character = characters.FirstOrDefault(character => character.Id == id);
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> CreateCharacter(CreateCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character newCharacter = _mapper.Map<Character>(character);
            newCharacter.Id = characters.Max(character => character.Id);

            characters.Add(_mapper.Map<Character>(character));
            serviceResponse.Data = characters.Select(character => _mapper.Map<GetCharacterDto>(character)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

                character.Name = updatedCharacter.Name;
                character.Defence = updatedCharacter.Defence;
                character.Strength = updatedCharacter.Strength;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Class = updatedCharacter.Class;
                character.Inteligence = updatedCharacter.Inteligence;

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }catch(NullReferenceException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
