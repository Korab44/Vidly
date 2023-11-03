using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
using Vidly.Migrations;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSubscribedToNewsLetter{ get; set; }
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
        [DataType(DataType.Date)]
        [Min18YearsIfAMember]
        public DateTime? Birthday { get; set; }
      
    }
}
