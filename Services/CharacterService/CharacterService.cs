using AutoMapper;
using DotNetRPG.Data;
using DotNetRPG.DTOs.Character;
using DotNetRPG.Models;
using Microsoft.EntityFrameworkCore;

namespace DotNetRPG.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();

            serviceResponse.Data = dbCharacters.Select(character => _mapper.Map<GetCharacterDto>(character)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(character => character.Id == id);

            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> CreateCharacter(CreateCharacterDto character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character newCharacter = _mapper.Map<Character>(character);

            _context.Characters.Add(_mapper.Map<Character>(character));
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters
                .Select(character => _mapper.Map<GetCharacterDto>(character))
                .ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = _context.Characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);

                character.Name = updatedCharacter.Name;
                character.Defence = updatedCharacter.Defence;
                character.Strength = updatedCharacter.Strength;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Class = updatedCharacter.Class;
                character.Inteligence = updatedCharacter.Inteligence;

                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            }catch(NullReferenceException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                var character = await _context.Characters.FirstAsync(c => c.Id == id);

                _context.Characters.Remove(character);

                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Characters.Select(character =>_mapper.Map<GetCharacterDto>(character)).ToListAsync();
            } 
            catch (NullReferenceException ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }
    }
}
