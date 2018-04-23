using Microsoft.AspNetCore.Antiforgery;
using EdwardAbp.Controllers;

namespace EdwardAbp.Web.Host.Controllers
{
    public class AntiForgeryController : EdwardAbpControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
