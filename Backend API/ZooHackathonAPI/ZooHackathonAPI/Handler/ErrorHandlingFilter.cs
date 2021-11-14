using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core.Exceptions;
using System.Threading.Tasks;
using ZooHackathonAPI.Responses;

namespace ZooHackathonAPI.Handler
{
    public class ErrorHandlingFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ParseException || context.Exception is ErrorResponse)
            {
                string message = context.Exception.ToString();
                if (context.Exception.GetType() == typeof(ErrorResponse)) message = ((ErrorResponse)context.Exception).Error.Message;
                context.Result = new ObjectResult(new ErrorResponse(((ErrorResponse)context.Exception).Error.Code, message))
                {
                    StatusCode = ((ErrorResponse)context.Exception).Error.Code
                };
                context.ExceptionHandled = true;
                return;
            }
        }
    }
}
