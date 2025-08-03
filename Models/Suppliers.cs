using System.ComponentModel.DataAnnotations;

namespace VehicleLeasingApp.Models
{
    public class Suppliers
    {
        [Key]
        public int SupplierId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string ContactPerson { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Vehicles> Vehicles { get; set; }

        public Suppliers()
        {
            Vehicles = new List<Vehicles>();
        }
    }
}
