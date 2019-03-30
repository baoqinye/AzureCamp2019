using System;
using System.IO;
using Microsoft.Data.DataView;
using Microsoft.ML;
using Microsoft.ML.Data;
using static Microsoft.ML.Transforms.NormalizingEstimator;

namespace ModelBuilder
{
    class Program
    {
        static readonly string _trainDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "WeeklyEarningsTrain.csv");
        static readonly string _testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "WeeklyEarningsTest.csv");
        static readonly string _modelPath = Path.Combine(Environment.CurrentDirectory, "Data", "Model.zip");
        static void Main(string[] args)
        {
            MLContext mlContext = new MLContext();

            // If working in Visual Studio, make sure the 'Copy to Output Directory'
            // property of iris-data.txt is set to 'Copy always'
            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<WeeklyEarnings>(path: _trainDataPath, hasHeader: false, separatorChar: ',');
            IDataView testDataView = mlContext.Data.LoadFromTextFile<WeeklyEarnings>(_testDataPath, hasHeader: false, separatorChar: ',');

            var dataProcessPipeline = mlContext.Transforms.CopyColumns(outputColumnName: DefaultColumnNames.Label, inputColumnName: nameof(WeeklyEarnings.AverageWeeklyEarnings))
                            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "GeographyEncoded", inputColumnName: nameof(WeeklyEarnings.Geography)))
                            .Append(mlContext.Transforms.Categorical.OneHotEncoding(outputColumnName: "IndustryEncoded", inputColumnName: nameof(WeeklyEarnings.Industry)))
                            .Append(mlContext.Transforms.Concatenate(DefaultColumnNames.Features, "GeographyEncoded", "IndustryEncoded"));
            var trainer = mlContext.Regression.Trainers.StochasticDualCoordinateAscent(labelColumnName: DefaultColumnNames.Label, featureColumnName: DefaultColumnNames.Features);
            var trainingPipeline = dataProcessPipeline.Append(trainer);
            Console.WriteLine("=============== Training the model ===============");
            var trainedModel = trainingPipeline.Fit(trainingDataView);
            
            Console.WriteLine("===== Evaluating Model's accuracy with Test data =====");

            IDataView predictions = trainedModel.Transform(testDataView);
            var metrics = mlContext.Regression.Evaluate(predictions, label: DefaultColumnNames.Label, score: DefaultColumnNames.Score);

            ConsoleHelper.PrintRegressionMetrics(trainer.ToString(), metrics);

        
           
            using (var fs = File.Create(_modelPath))
                trainedModel.SaveTo(mlContext, fs);

            Console.WriteLine("The model is saved to {0}", _modelPath);
            
            Console.WriteLine("Testing single");
            TestSinglePrediction(mlContext);
            Console.ReadLine();
        }

        private static void TestSinglePrediction(MLContext mlContext)
        {
            //Sample: 

            //48,British Columbia,Average weekly earnings including overtime for all employees,Other services (except public administration),580.13,-7.5,0.7

            var weeklyEarning = new WeeklyEarnings()
            {
                Date = 48,
                Geography = "British Columbia",
                Industry = "Other services (except public administration)"
            };

            ITransformer trainedModel;
            using (var stream = new FileStream(_modelPath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                trainedModel = mlContext.Model.Load(stream);
            }

            // Create prediction engine related to the loaded trained model
            var predEngine = trainedModel.CreatePredictionEngine<WeeklyEarnings, WeeklyEarningsPrediction>(mlContext);

            //Score
            var resultprediction = predEngine.Predict(weeklyEarning);
            ///

            Console.WriteLine($"**********************************************************************");
            Console.WriteLine($"Predicted weekly earning: {resultprediction.AverageWeeklyEarnings:0.####}, actual weekly earning: 580.13");
            Console.WriteLine($"**********************************************************************");
        }
    }
}
