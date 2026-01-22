using System.ComponentModel.DataAnnotations;

namespace Final_Seyd_NediBudi.ViewModels.PositionViewModesl
{
    public class PositionCreateVM
    {
        [Required, MaxLength(256), MinLength(3)]
        public string Name { get; set; } = string.Empty;
    }
}
