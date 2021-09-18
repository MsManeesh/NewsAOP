using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Service.Exceptions;
using System;
using System.Net;

namespace NewsAPI.Aspect
{
    public class ExceptionHandler : ExceptionFilterAttribute
    {
        /* Inherit the ExceptionFilterAttribute */

        /*Override the method OnException uisng ExceptionFilterAttribute to handle the exceptions.
        */
        public override void OnException(ExceptionContext Context)
        {
            if (!Context.ExceptionHandled)
            {
                string typeName = Context.Exception.GetType().Name;
                HttpStatusCode response = HttpStatusCode.InternalServerError;
                switch (typeName)
                {
                    case "NewsAlreadyExistsException":
                    case "ReminderAlreadyExistsException":
                    case "UserAlreadyExistsException":
                        response=HttpStatusCode.Conflict;
                        break;
                    case "NewsNotFoundException":
                    case "UserNotFoundException":
                    case "ReminderNotFoundException":
                        response = HttpStatusCode.NotFound;
                        break;
                    default:
                        response = HttpStatusCode.InternalServerError;
                        break;
                }
                Context.HttpContext.Response.StatusCode = (int)response;
                //Context.HttpContext.Response.ContentType = "text/pain";
                Context.Result= new ObjectResult(Context.Exception.Message);
                Context.ExceptionHandled = true;
                
            }
        }
    }
}
