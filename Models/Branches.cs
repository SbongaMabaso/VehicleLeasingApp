using System.ComponentModel.DataAnnotations;

namespace VehicleLeasingApp.Models
{
    public class Branches
    {
        [Key]
        public int BranchId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Location { get; set; }

        public virtual ICollection<Vehicles> Vehicles { get; set; }

        public Branches()
        {
            Vehicles = new List<Vehicles>();
        }
    }
}
