namespace RookieRider.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using RookieRider.Utility;

    public class ErrorController : Controller
    {
        [Route("/Error/{statusCode}")]
        public IActionResult StatusCodeViewBinder(int statusCode)
        {
            string route = string.Empty;
            ViewData["ErrorMessage"] = $"Seems like you ran out of fuel... {Emoji.Sob}";
            switch (statusCode)
            {
                case 404:
                    route = "404";
                    break;
            }

            return View(route);
        }
    }
}
