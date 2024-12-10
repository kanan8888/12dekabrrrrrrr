using Microsoft.AspNetCore.Mvc;
using Uniqlo.Areas.Manage.Common;

namespace Uniqlo.Areas.Manage.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
