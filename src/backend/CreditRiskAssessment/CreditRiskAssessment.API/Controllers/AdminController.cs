using Microsoft.AspNetCore.Mvc;

namespace CreditRiskAssessment.API.Controllers
{
    [ApiController]
    [Route("admin/[Action]")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
