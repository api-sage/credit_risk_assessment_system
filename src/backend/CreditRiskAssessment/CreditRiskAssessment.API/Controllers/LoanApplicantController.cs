using CreditRiskAssessment.Interfaces;
using CreditRiskAssessment.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace CreditRiskAssessment.API.Controllers
{
    [ApiController]
    [Route("apply/[Action]")]
    public class LoanApplicantController : Controller
    {
        private ICheckCreditWorthinessService _checkCreditWorthinessService;
        private Serilog.ILogger _logger;

        public LoanApplicantController(ICheckCreditWorthinessService checkCreditWorthinessService, Serilog.ILogger logger)
        {
            _checkCreditWorthinessService = checkCreditWorthinessService;
            _logger = logger;

        }

        //[HttpGet]
        //public async Task<ActionResult> TrainModel()
        //{
        //    var response = await _crasPredictService.TrainModelAsync();
        //    return Ok(response);
        //}

        [HttpPost]
        public async Task<ActionResult> AssessCreditWorthiness(CheckCreditWorthinessRequest request)
        {
            _logger.Information(request.ToString());
            var response = await _checkCreditWorthinessService.CheckCreditWorthiness(request);
            _logger.Information(response.ToString());
            return Ok(response);
        }
    }
}
