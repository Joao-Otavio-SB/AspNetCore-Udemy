using DotNetRPG.DTOs.Character;
using DotNetRPG.Models;
using DotNetRPG.Services.CharacterService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetRPG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> GetCharacter()
        {
            return Ok(await _characterService.GetCharacters());
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetCharacterById(int id)
        {
            var character = await _characterService.GetCharacterById(id);

            return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> CreateCharacter(CreateCharacterDto character)
        {
            return Ok(await _characterService.CreateCharacter(character));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto character)
        {
            var serviceResponse = await _characterService.UpdateCharacter(character);

            if(serviceResponse.Data == null)
            {
                return NotFound(serviceResponse);
            }

            return Ok(serviceResponse);
        }

    }
}
