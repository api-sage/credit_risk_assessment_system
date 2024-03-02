using CreditRiskAssessment.Infrastructure.Commons;
using CreditRiskAssessment.ML.Interfaces;
using CreditRiskAssessment.ML.Models;
using Microsoft.ML;
using Microsoft.ML.AutoML;
using Microsoft.ML.Data;
using Newtonsoft.Json;
using Serilog;

namespace CreditRiskAssessment.ML.Services;

public class CRASPredictService : ICRAS_Service
{
    private MLContext _mLContext;
    private DataViewSchema _dataViewSchema;
    private ITransformer _creditScorePipeline;
    private PredictionEngine<LoanApplicantRequest, LoanApplicantMLResponse> _creditScoreEngine;
    private string analyticalDataPath;
    private ILogger _logger;
    public CRASPredictService(ILogger logger)
    {
        _logger = logger;
        _mLContext = new MLContext();
        _creditScorePipeline = _mLContext.Model.Load("CRASML.zip", out _dataViewSchema);
        //CREATES ASSESSMENT ENGINE TO ASSESS CREDIT WORTHINESS
        _creditScoreEngine = _mLContext.Model.CreatePredictionEngine<LoanApplicantRequest, LoanApplicantMLResponse>(_creditScorePipeline);
    }

    //ONLY CALL THIS METHOD TO RE-TRAIN THE MODEL SHOULD YOU HAVE NEW SET OF ANALYTICAL DATASET
    //THIS METHOD TRAINS THE MACHINE LEARNING MODEL USING THE cleaned_again.csv DATASET
    //AND PROCEEDS TO SAVE IT AS CRAS.zip FILE IN THE API DIRECTORY
    public async Task<ResponseResult<string>> TrainModelAsync()
    {
        ResponseResult<string> response = new ResponseResult<string>();

        //INITIALIZES MODEL ACCURACY MEASURE IN PERCENTAGE BASED ON RESULTS OF THE 20% TEST DATASET
        double accuracyMeasure = 0;

        try
        {
            //PATH TO CLEANED ANALYTICAL DATASET
            analyticalDataPath = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "CRAS_data_ML.csv");

            //READS THROUGH THE CLEANED DATASET AND INFERS MORE INFORMATION FROM IT FOR EACH COLUMN. INFORMATION SUCH AS DATA TYPE, QUOTED STRINGS, ETC
            ColumnInferenceResults columnInferenceResult = _mLContext.Auto().InferColumns(analyticalDataPath, labelColumnName: "CreditScore", groupColumns: false);

            //LOADS INFORMATION MADE FROM COLUMN INFERENCES INTO THE TEXT LOADER
            TextLoader textLoader = _mLContext.Data.CreateTextLoader(columnInferenceResult.TextLoaderOptions);

            //DEFINES A DATVIEW FORM THE TEXT LOADER
            IDataView dataView = textLoader.Load(analyticalDataPath);

            //DIVIDES THE DATASET INTO TWO - 80% FOR TRAINING AND 20% FOR TESTING
            var trainTestData = _mLContext.Data.TrainTestSplit(dataView, testFraction: 0.2);

            //BUILDS ML PIPELINE FOR DATA TRANSFORMATION USING AUTOML
            SweepablePipeline pipeline = _mLContext.Auto().Featurizer(dataView, columnInformation: columnInferenceResult.ColumnInformation)
                .Append(_mLContext.Auto().Regression(labelColumnName: columnInferenceResult.ColumnInformation.LabelColumnName));

            //SETS MODEL TRAINING, DESIRED METRIC PARAMETERS, TRAINING TIME AND DATASET FOR TRAINING PURPOSE
            AutoMLExperiment autoMLExperiment = _mLContext.Auto().CreateExperiment();
            autoMLExperiment
                .SetPipeline(pipeline)
                .SetRegressionMetric(metric: RegressionMetric.RSquared, labelColumn: columnInferenceResult.ColumnInformation.LabelColumnName)
                .SetTrainingTimeInSeconds(3000)
                .SetDataset(trainTestData);

            //LOGS TELEMETRY EVENTS AS MODEL IS BEING TRAINED LOOKING OUT FOR KEY-WORD "AutoMLExperiment"
            _mLContext.Log += (_, e) =>
            {
                if (e.Source.Equals("AutoMLExperiment"))
                {
                    _logger.Information($"Model Training in Progress:: {e.Message}");
                }
            };

            //THIS TRIGGERS THE TRAINING OF THE MODEL
            TrialResult results = await autoMLExperiment.RunAsync();

            //CAPTURES THE MODEL FROM THE results VARIABLE
            ITransformer trainedModel = results.Model;

            _logger.Information(string.Concat("CRAS Accuracy:: ", results.Metric.ToString()));

            //SAVES TRAINED MODEL IN THE CRAS.zip FILE
            _mLContext.Model.Save(trainedModel, dataView.Schema, "CRAS.zip");

            accuracyMeasure = results.Metric * 100;
        }
        catch (Exception ex)
        {
            //LOGS ERROR MESSGAE
            _logger.Error(ex.Message);
            response.status = Constants.ERROR;
            response.message = ex.Message;
            return response;
        }
        response.status = Constants.SUCCESS;
        response.message = $"Model training completed successfully with {accuracyMeasure}% accuracy";
        return response;
    }

    //THIS METHOD ASSESSES THE CREDIT WORHTINESS OF A LOAN APPLICANT
    public async Task<ResponseResult<LoanApplicantMLResponse>> CalculateCreditScore(LoanApplicantRequest request)
    {
        //INSTATIATES RESPONSE FRAMEWORK
        ResponseResult<LoanApplicantMLResponse> response = new ResponseResult<LoanApplicantMLResponse>();

        try
        {
            _logger.Information($"Model Request :: {JsonConvert.SerializeObject(request)}");

            //ASSESSES LOAN APPLICANT'S CREDIT WORTHINESS AND RETURNS METRIC RESULTS
            //PredictedLoanStatus IS TRUE IF THE MODEL DECIDES, BASED ON TRAINING, THAT A LOAN APPLICANT IS WORHTY OF A LOAN AMOUNT
            //PredictedLoanStatus IS FALSE IF THE MODEL DECIDES, BASED ON TRAINING, THAT A LOAN APPLICANT IS NOT WORTHY OF A LOAN AMOUNT
            //OTHER METRIC PARAMETERS SUCH AS Probability AND Score ARE PROVIDED AS WELL
            LoanApplicantMLResponse calculateCreditScore = _creditScoreEngine.Predict(request);
            _logger.Information($"Model Response:: {JsonConvert.SerializeObject(calculateCreditScore)}");
            response.data = calculateCreditScore;
        }
        catch (Exception ex)
        {
            _logger.Error(ex.Message);
            response.status = Constants.ERROR;
            response.message = ex.Message;
            return response;
        }
        response.status = Constants.SUCCESS;
        response.message = $"Credit score predicted successfully";
        return response;
    }
}
