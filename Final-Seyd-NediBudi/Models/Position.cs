using Final_Seyd_NediBudi.Models.Common;

namespace Final_Seyd_NediBudi.Models
{
    public class Position : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public ICollection<Employee> Employees { get; set; } = [];
    }
}
