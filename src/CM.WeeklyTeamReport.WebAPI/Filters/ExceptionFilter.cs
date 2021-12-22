using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CM.WeeklyTeamReport.Domain.Exceptions;

namespace CM.WeeklyTeamReport.WebAPI.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is DbRecordNotFoundException)
            {
                context.Result = new NotFoundObjectResult(context.Exception.Message);
                context.ExceptionHandled = true;
            }
            if (context.Exception is System.Data.SqlClient.SqlException)
            {
                context.Result = new StatusCodeResult((int)HttpStatusCode.InternalServerError);
                context.ExceptionHandled = true;
            }
        }
    }
}
