//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Authorization;
//using Microsoft.AspNetCore.Mvc.Controllers;
//using Microsoft.AspNetCore.Mvc.Filters;
//using System.Security.Claims;

//Another way to make all controllers to need authorization 
//namespace Vidly.Helpers
//{
//    public class CustomAuthorizeFilter : IAuthorizationFilter
//    {
//        public void OnAuthorization(AuthorizationFilterContext context)
//        {

//            if (!context.HttpContext.User.Identity.IsAuthenticated)
//            {
//                // Check if the current request is already the login page
//                var controllerName = context.ActionDescriptor.RouteValues["controller"];
//                var actionName = context.ActionDescriptor.RouteValues["action"];
//                var controllerDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
//                var allowAnonymousController = controllerDescriptor.ControllerTypeInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), inherit: true).Any();
//                var allowAnonymousAction = controllerDescriptor.MethodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), inherit: true).Any();

//                if (!allowAnonymousController && !allowAnonymousAction)
//                {
//                    // Redirect to the login page
//                    context.Result = new RedirectToActionResult("LogIn", "Account", null);
//                }
//            }
//        }
//    }
//}

