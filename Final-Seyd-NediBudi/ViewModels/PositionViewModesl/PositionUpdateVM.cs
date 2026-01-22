using System.ComponentModel.DataAnnotations;

namespace Final_Seyd_NediBudi.ViewModels.PositionViewModesl
{
    public class PositionUpdateVM
    {
        public int Id { get; set; }

        [Required, MaxLength(256), MinLength(3)]
        public string Name { get; set; } = string.Empty;
    }
}
