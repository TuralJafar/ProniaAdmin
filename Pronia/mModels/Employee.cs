using Microsoft.Build.Framework;

namespace Pronia.mModels
{
    public class Employee
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
