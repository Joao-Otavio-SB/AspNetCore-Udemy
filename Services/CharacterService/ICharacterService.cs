using DotNetRPG.DTOs.Character;
using DotNetRPG.Models;

namespace DotNetRPG.Services.CharacterService
{
    public interface ICharacterService
    {   
        Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters();
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        Task<ServiceResponse<List<GetCharacterDto>>> CreateCharacter(CreateCharacterDto character);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto character);
    }
}
