using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System.Data;

namespace RecipeRecommendationAlgorithmProcess
{
    class Program
    {
        static void Main(string[] args)
        {
            DataTable table = new DataTable();
            table.Columns.Add("UserId", typeof(float));
            table.Columns.Add("RecipeId", typeof(float));
            table.Columns.Add("Rating", typeof(float));

            Console.WriteLine(Convert.ToDouble("andra"));

            using (LicentaEntities context = new LicentaEntities())
            {
                var query = from f in context.Favorites
                            join u in context.Users on f.UserId equals u.Username
                            select new { u.ID, f.RecipeId, f.Rating };

                query.ToList().ForEach((x) =>
                {
                    DataRow row = table.NewRow();

                    row.SetField<float>("UserId", (float)Convert.ToDouble(x.ID));
                    row.SetField<float>("RecipeId", (float)x.RecipeId);
                    row.SetField<float>("Rating", (float)x.Rating);

                    table.Rows.Add(row);
                });
            }

            ExportData.ToCSV(table, Path.Combine(Environment.CurrentDirectory, "Data", "train.csv"));

            MLContext mlContext = new MLContext();

            IDataView trainingDataView = mlContext.Data.LoadFromTextFile<RecipeRating>(Path.Combine(Environment.CurrentDirectory, "Data", "train.csv"), hasHeader: true, separatorChar: ',');
            IDataView testDataView = mlContext.Data.LoadFromTextFile<RecipeRating>(Path.Combine(Environment.CurrentDirectory, "Data", "test.csv"), hasHeader: true, separatorChar: ',');

            IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "UserIdEncoded", inputColumnName: "UserId")
            .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "RecipeIdEncoded", inputColumnName: "RecipeId"));

            var options = new MatrixFactorizationTrainer.Options
            {
                MatrixColumnIndexColumnName = "UserIdEncoded",
                MatrixRowIndexColumnName = "RecipeIdEncoded",
                LabelColumnName = "Rating",
                NumberOfIterations = 20,
                ApproximationRank = 100
            };

            var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

            Console.WriteLine("------------ Train model ------------");
            ITransformer model = trainerEstimator.Fit(trainingDataView);


            //Console.WriteLine("------------ Evaluate model ------------");
            //var prediction = model.Transform(testDataView);

            //var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Rating", scoreColumnName: "Score");

            //Console.WriteLine("Root Mean Squared Error : " + metrics.RootMeanSquaredError.ToString());
            //Console.WriteLine("RSquared: " + metrics.RSquared.ToString());


            Console.WriteLine("------------ Making prediction ------------");
            var predictionEngine = mlContext.Model.CreatePredictionEngine<RecipeRating, RecipeRatingPrediction>(model);

            RecipeRating testInput = new RecipeRating();

            using (LicentaEntities entities = new LicentaEntities())
            {
                var UserIds = from f in entities.Favorites select f.UserId;
                var RecipeIds = from r in entities.Favorites select r.RecipeId;

                foreach(var user in UserIds)
                {
                    testInput.UserId = (float)Convert.ToDouble(user);
                    string recommendedRecipes = "";
                    Recommendation recommendation = new Recommendation();

                    foreach (var recipe in RecipeIds)
                    {
                        testInput.RecipeId = recipe;
                        
                        var movieRatingPrediction = predictionEngine.Predict(testInput);

                        if (Math.Round(movieRatingPrediction.Score, 1) > 3.5)
                        {
                            recommendedRecipes += testInput.RecipeId + ",";
                        }
                    }

                    recommendation.UserId = user;
                    recommendation.Recommendations = recommendedRecipes.Remove(recommendedRecipes.Length - 1);

                    entities.Recommendations.Add(recommendation);
                    entities.SaveChanges();
                }

            }


            Console.WriteLine("------------ Save model to file ------------");
            mlContext.Model.Save(model, trainingDataView.Schema, Path.Combine(Environment.CurrentDirectory, "Data", "model.zip"));


            Console.ReadLine();
         
        }

    }
    public static class ExportData
    {
        public static void ToCSV(this DataTable table, string path)
        {
            StreamWriter stream = new StreamWriter(path, false);

            for (int i = 0; i < table.Columns.Count; i++)
            {
                stream.Write(table.Columns[i]);
                if (i < table.Columns.Count - 1)
                {
                    stream.Write(",");
                }
            }

            stream.Write(stream.NewLine);

            foreach (DataRow row in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(row[i]))
                    {
                        string value = row[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            stream.Write(value);
                        }
                        else
                        {
                            stream.Write(row[i].ToString());
                        }
                    }
                    if (i < table.Columns.Count - 1)
                    {
                        stream.Write(",");
                    }
                }
                stream.Write(stream.NewLine);
            }
            stream.Close();
        }

    }
}
