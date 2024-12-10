using Microsoft.AspNetCore.Mvc;
using Uniqlo.Areas.Manage.Common;
using Uniqlo.DAL;

namespace Uniqlo.Areas.Manage.Controllers
{
    public class CategoryController:BaseController
    {
        AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View(_context.Categories);
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}
