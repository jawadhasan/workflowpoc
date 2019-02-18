using Microsoft.AspNetCore.Mvc;
using Workflow.WebUi.Infrastructure;

namespace Workflow.WebUi.Controllers
{
    public class BaseController : Controller
    {
        protected new IActionResult Ok()
        {
            return base.Ok(Envelop.Ok());
        }

        protected IActionResult Ok<T>(T result)
        {
            return base.Ok(Envelop.Ok(result));
        }

        protected IActionResult Error(string errorMessage)
        {
            return BadRequest(Envelop.Error(errorMessage));
        }
    }
}
