using DotNetRPG.Models;

namespace DotNetRPG.DTOs.Character
{
    public class CreateCharacterDto
    {
        public string Name { get; set; }
        public int HitPoints { get; set; }
        public int Strength { get; set; }
        public int Defence { get; set; }
        public int Inteligence { get; set; }
        public RpgClass Class { get; set; }
    }
}
