using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeeManagement.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }


        [Route("/Error/{StatusCode}")]
        public IActionResult HTTPStatusCodeHandler(int StatusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (StatusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry request resource is not available.";
                    logger.LogWarning($"404 Error occured. Path = {statusCodeResult.OriginalPath}" +
                        $"and QueryString = {statusCodeResult.OriginalQueryString} ");
                //    ViewBag.Path = statusCodeResult.OriginalPath;
                //    ViewBag.QS=statusCodeResult.OriginalQueryString;
                    break;
                //case 500:
                //    ViewBag.ErrorMessage = "Sorry resource not found.";
                //    break;
                //case 200:
                //    ViewBag.ErrorMessage = "Sorry resource not found.";
                //    break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails=HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            logger.LogError($"The path {exceptionDetails.Path} threw an exception {exceptionDetails.Error}");
            //ViewBag.ExceptionPath = exceptionDetails.Path;
            //ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            //ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;

            return View("Error");
        }

    }
}
