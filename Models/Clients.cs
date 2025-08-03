using System.ComponentModel.DataAnnotations;

namespace VehicleLeasingApp.Models
{
    public class Clients
    {
        [Key]
        public int ClientId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        public string ContactPerson { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Vehicles> Vehicles { get; set; }
        public Clients()
        {
            Vehicles = new List<Vehicles>();
        }

    }
}
