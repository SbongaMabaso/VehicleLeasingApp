using System.ComponentModel.DataAnnotations;

namespace VehicleLeasingApp.Models
{
    public class Manufacturers
    {
        [Key]
        public int ManufacturerId { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<Vehicles> Vehicles { get; set; }

        public Manufacturers()
        {
            Vehicles = new List<Vehicles>();
        }
    }
}
