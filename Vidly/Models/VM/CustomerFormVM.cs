namespace Vidly.Models.VM
{
    public class CustomerFormVM
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customers { get; set; }
    }
}
