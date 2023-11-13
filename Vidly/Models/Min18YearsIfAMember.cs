//using System;
//using System.ComponentModel.DataAnnotations;

//namespace Vidly.Models
//{
//    public class Min18YearsIfAMember : ValidationAttribute
//    {
//        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
//        {


//            var customer = (Customer)validationContext.ObjectInstance;
//            if (customer.MembershipTypeId == MembershipType.Unknown
//                || customer.MembershipTypeId == MembershipType.PayAsYouGo)
//            {
//                return ValidationResult.Success;
//            }
//            if (customer.Birthday == null)
//            {
//                throw new Exception("Please enter birthday");
//            }
//            var age = DateTime.Now.Year - customer.Birthday.Value.Year;
//            if (age <= 18)
//            {
//                throw new Exception("Customer should be at least 18 years old to go on a membership");

//            }
//            return ValidationResult.Success;
//        }
//    }
//}
