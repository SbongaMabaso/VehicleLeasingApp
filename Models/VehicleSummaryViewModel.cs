namespace VehicleLeasingApp.Models
{
    public class VehicleSummaryViewModel
    {
        public List<SupplierSummary> supplierSummaries { get; set; }
        public List<BranchSummary> branchSummaries { get; set; }
        public List<ClientSummary> clientSummaries { get; set; }
    }

    public class SupplierSummary
    {
        public string Supplier { get; set; }
        public string Manufacturer { get; set; }
        public int Count { get; set; }
    }

    public class BranchSummary
    {
        public string Branch { get; set; }
        public string Manufacturer { get; set; }
        public int Count { get; set; }
    }

    public class ClientSummary
    {
        public string Client { get; set; }
        public string Manufacturer { get; set; }
        public int Count { get; set; }
    }
}
