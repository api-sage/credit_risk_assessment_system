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
    private ITransformer _creditAssessmentPipeline;
    private PredictionEngine<LoanApplicantRequest, LoanApplicantMLResponse> _assessmentEngine;
    private string analyticalDataPath;
    private ILogger _logger;
    public CRASPredictService(ILogger logger)
    {
        _logger = logger;
        _mLContext = new MLContext();
        _creditAssessmentPipeline = _mLContext.Model.Load("CRAS.zip", out _dataViewSchema);
        _assessmentEngine = _mLContext.Model.CreatePredictionEngine<LoanApplicantRequest, LoanApplicantMLResponse>(_creditAssessmentPipeline);
    }

    //ONLY CALL THIS METHOD TO RE-TRAIN THE MODEL SHOULD YOU HAVE NEW SET OF ANALYTICAL DATASET
    public async Task<string> TrainModelAsync()
    {
        analyticalDataPath = Path.Combine(Directory.GetCurrentDirectory(), "Helpers", "cleaned_again.csv");
        ColumnInferenceResults columnInferenceResult = _mLContext.Auto().InferColumns(analyticalDataPath, labelColumnName: "LoanStatus", groupColumns: false);
        TextLoader textLoader = _mLContext.Data.CreateTextLoader(columnInferenceResult.TextLoaderOptions);
        IDataView dataView = textLoader.Load(analyticalDataPath);
        var trainTestData = _mLContext.Data.TrainTestSplit(dataView, testFraction: 0.2);
        SweepablePipeline pipeline = _mLContext.Auto().Featurizer(dataView, columnInformation: columnInferenceResult.ColumnInformation)
            .Append(_mLContext.Auto().BinaryClassification(labelColumnName: columnInferenceResult.ColumnInformation.LabelColumnName));
        AutoMLExperiment autoMLExperiment = _mLContext.Auto().CreateExperiment();
        autoMLExperiment
            .SetPipeline(pipeline)
            .SetBinaryClassificationMetric(metric: BinaryClassificationMetric.F1Score, labelColumn: columnInferenceResult.ColumnInformation.LabelColumnName)
            .SetBinaryClassificationMetric(metric: BinaryClassificationMetric.Accuracy, labelColumn: columnInferenceResult.ColumnInformation.LabelColumnName)
            .SetTrainingTimeInSeconds(600)
            .SetDataset(trainTestData);
        _mLContext.Log += (_, e) =>
        {
            if (e.Source.Equals("AutoMLExperiment"))
            {
                _logger.Information($"Model Training in Progress:: {e.Message}");
            }
        };
        TrialResult results = await autoMLExperiment.RunAsync();
        ITransformer trainedModel = results.Model;
        _logger.Information(results.Metric.ToString());
        _logger.Information(string.Concat("CRAS Accuracy:: ", results.Metric.ToString()));
        _mLContext.Model.Save(trainedModel, dataView.Schema, "CRAS.zip");
        return $"Model training completed successfully with {results.Metric*100}% accuracy";
    }

    //THIS METHOD ASSESSES THE CREDIT WORHTINESS OF A LOAN APPLICANT
    public LoanApplicantMLResponse AssessCreditWorthiness(LoanApplicantRequest request)
    {
        _logger.Information($"Model Request :: {JsonConvert.SerializeObject(request)}");
        LoanApplicantMLResponse assessCreditWorthiness = _assessmentEngine.Predict(request);
        _logger.Information($"Model Response:: {JsonConvert.SerializeObject(assessCreditWorthiness)}");
        return assessCreditWorthiness;
    }
}
