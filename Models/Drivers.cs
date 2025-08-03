using System.ComponentModel.DataAnnotations;

namespace VehicleLeasingApp.Models
{
    public class Drivers
    {
        [Key]
        public int DriverId { get; set; }

        [Required]
        public string FullName { get; set; }

        public string LicenseNumber { get; set; }
        public DateTime LicenseExpiry { get; set; }

        public virtual ICollection<Vehicles> Vehicles { get; set; }

        public Drivers()
        {
            Vehicles = new List<Vehicles>();
        }
    }
}
