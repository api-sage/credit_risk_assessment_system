using Microsoft.AspNetCore.Mvc;

namespace CreditRiskAssessment.API.Controllers
{
    [ApiController]
    [Route("apply/[Action]")]
    public class LoanApplicantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
