using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using System;

namespace MOBILE_PHONE.Attributes {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RestrictAuthenticatedUserAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(ActionExecutingContext context){
            var user = context.HttpContext.User;
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (user.Identity.IsAuthenticated){
                context.Result = new RedirectToActionResult("Index", "Dashboard", null);
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            base.OnActionExecuting(context);
        }

    }
}