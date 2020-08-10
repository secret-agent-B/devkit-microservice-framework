// -----------------------------------------------------------------------
// <copyright file="PipelineFilterAttribute.cs" company="Adriano">
//      See the [assembly: AssemblyCopyright(..)] marking attribute linked in to this file's associated project for copyright © information.
// </copyright>
// -----------------------------------------------------------------------

namespace Devkit.WebAPI.Filters
{
    using System;
    using System.Linq;
    using System.Net;
    using Devkit.Patterns.Exceptions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Custom exception filter attribute.
    /// </summary>
    /// <seealso cref="ExceptionFilterAttribute" />
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class PipelineFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Finds the innermost exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>
        /// An exception.
        /// </returns>
        public static Exception FindInnermostException(Exception exception)
        {
            while (true)
            {
                if (exception?.InnerException == null)
                {
                    return exception;
                }

                exception = exception.InnerException;
            }
        }

        /// <summary>
        /// Catches validation issues that is triggered by RequestValidationBehavior.
        /// </summary>
        /// <param name="context">The exception context.</param>
        /// <inheritdoc />
        public override void OnException(ExceptionContext context)
        {
            const string _json_mime_type = "application/json";

            if (context?.Exception is RequestException exception)
            {
                context.HttpContext.Response.ContentType = _json_mime_type;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Result = new JsonResult(new
                {
                    errors = exception.Errors.Values.SelectMany(x => x)
                });

                return;
            }

            var code = HttpStatusCode.InternalServerError;

            if (context.Exception is NotFoundException)
            {
                code = HttpStatusCode.NotFound;
            }

            context.HttpContext.Response.ContentType = _json_mime_type;
            context.HttpContext.Response.StatusCode = (int)code;

            var rootCause = FindInnermostException(context.Exception);

            context.Result = new JsonResult(new
            {
                errors = new[] { rootCause.Message },
                stackTrace = rootCause.StackTrace
            });
        }
    }
}