//using Vidly.Data;
//using Vidly.Models;

//namespace Vidly.Helpers
//{
//    public class GetMembershipType : AppDbContext 
//    {
//        private readonly AppDbContext _context;

//        public GetMembershipType(AppDbContext context)
//        {
//            _context = context;
//        }

//        public MembershipType GetMembershipTypeById(int id)
//        {
//            var result = _context.MembershipTypes.FirstOrDefault(t => t.Id == id);
//            return result;
//        }
//    }
//}
