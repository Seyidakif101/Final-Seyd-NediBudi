using Final_Seyd_NediBudi.Models.Common;

namespace Final_Seyd_NediBudi.Models
{
    public class Employee:BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string ImagePath { get; set; } = string.Empty;
        public int PositionId { get; set; }
        public Position Position { get; set; } = null!;
    }
}
