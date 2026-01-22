using System.ComponentModel.DataAnnotations;

namespace Final_Seyd_NediBudi.ViewModels.EmployeeViewModels
{
    public class EmployeeCreateVM
    {
        [Required,MaxLength(256),MinLength(3)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public IFormFile Image { get; set; } = null!;
        [Required]
        public int PositionId { get; set; }
    }
}
