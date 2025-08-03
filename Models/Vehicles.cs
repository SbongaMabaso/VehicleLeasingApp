using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VehicleLeasingApp.Models
{
    public class Vehicles
    {
        [Key]
        public int VehicleId { get; set; }

        [Required]
        public string RegistrationNumber { get; set; }

        public string Model { get; set; }

        public int ManufacturerId { get; set; }
        [ForeignKey("ManufacturerId")]
        public virtual Manufacturers Manufacturer { get; set; }

        public int SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public virtual Suppliers Supplier { get; set; }

        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public virtual Branches Branch { get; set; }

        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual Clients Client { get; set; }

        public int DriverId { get; set; }
        [ForeignKey("DriverId")]
        public virtual Drivers Driver { get; set; }

        public DateTime LeaseStartDate { get; set; }
        public DateTime? LeaseEndDate { get; set; }
    }
}
