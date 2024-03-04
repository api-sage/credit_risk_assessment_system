using CreditRiskAssessment.Entities;
using CreditRiskAssessment.Infrastructure.Commons;
using CreditRiskAssessment.Interfaces;
using CreditRiskAssessment.ML.Interfaces;
using CreditRiskAssessment.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;

namespace CreditRiskAssessment.API.Controllers
{
    [ApiController]
    [Route("/[Action]")]
    public class CreditHistoryController : Controller
    {
        private ICheckCreditWorthinessService _checkCreditWorthinessService;
        private ICRAS_Service _crasService;
        private Serilog.ILogger _logger;

        public CreditHistoryController(ICheckCreditWorthinessService checkCreditWorthinessService, ICRAS_Service crasService, Serilog.ILogger logger)
        {
            _checkCreditWorthinessService = checkCreditWorthinessService;
            _crasService = crasService;
            _logger = logger;
        }

        //ONLY CALL THIS METHOD WHEN YOU WANT TO RE-TRAIN THE CRAS.zip MACHINE LEARNING MODEL
        //TO ACCESS THIS METHOD FOR THE ABOVE PURPOSE, SIMPLY UNCOMMENT IT

        [HttpGet]
        [SwaggerOperation(Summary = "Re-trains the Machine Learning model")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(string))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TrainModel()
        {
            var response = await _crasService.TrainModelAsync();
            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Assesses the credit worthiness of loan applicants based on their credit history")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(ResponseResult<AssessRiskLevelResponse>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AssessCreditHistory(string bvn)
        {
            _logger.Information($"Client Request:: {JsonConvert.SerializeObject(bvn)}");
            ResponseResult<AssessRiskLevelResponse> response = await _checkCreditWorthinessService.AssessRiskLevel(bvn);
            _logger.Information($"API Response:: {JsonConvert.SerializeObject(response)}");
            return Ok(response);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Fetches the assessed credit history of an entity")]
        [SwaggerResponse(StatusCodes.Status200OK, "Request successful", typeof(ResponseResult<List<AssessedCustomer>>))]
        [SwaggerResponse(StatusCodes.Status400BadRequest)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAssessedCreditHistory(string bvn)
        {
            _logger.Information($"Client Request:: {JsonConvert.SerializeObject(bvn)}");
            ResponseResult<List<AssessedCustomer>> response = await _checkCreditWorthinessService.GetAssessedCreditHistory(bvn);
            _logger.Information($"API Response:: {JsonConvert.SerializeObject(response)}");
            return Ok(response);
        }
    }
}
