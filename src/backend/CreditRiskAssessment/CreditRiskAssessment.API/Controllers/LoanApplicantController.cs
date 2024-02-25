using CreditRiskAssessment.Interfaces;
using CreditRiskAssessment.ML.Interfaces;
using CreditRiskAssessment.Models.Request;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CreditRiskAssessment.API.Controllers
{
    [ApiController]
    [Route("apply/[Action]")]
    public class LoanApplicantController : Controller
    {
        private ICheckCreditWorthinessService _checkCreditWorthinessService;
        private ICRAS_Service _crasService;
        private Serilog.ILogger _logger;

        public LoanApplicantController(ICheckCreditWorthinessService checkCreditWorthinessService, ICRAS_Service crasService, Serilog.ILogger logger)
        {
            _checkCreditWorthinessService = checkCreditWorthinessService;
            _crasService = crasService;
            _logger = logger;

        }

        //ONLY CALL THIS METHOD WHEN YOU WANT TO RE-TRAIN THE CRAS.zip MACHINE LEARNING MODEL
        //TO ACCESS THIS METHOD FOR THE ABOVE PURPOSE, SIMPLY UNCOMMENT IT

        //[HttpGet]
        //public async Task<IActionResult> TrainModel()
        //{
        //    var response = await _crasService.TrainModelAsync();
        //    return Ok(response);
        //}

        [HttpPost]
        public async Task<IActionResult> AssessCreditWorthiness(CheckCreditWorthinessRequest request)
        {
            _logger.Information($"Client Request:: {JsonConvert.SerializeObject(request)}");
            var response = await _checkCreditWorthinessService.CheckCreditWorthiness(request);
            _logger.Information($"API Response:: {JsonConvert.SerializeObject(response)}");
            return Ok(response);
        }
    }
}
