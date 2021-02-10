using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Trainers;
using System.Data;
using XPlot.Plotly;

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

            using (LicentaEntities entities = new LicentaEntities())
            {
                for (int i = 0; i < 50; i++)
                {
                    entities.Users.Add(new User { Username = "test" + i.ToString() });
                    entities.SaveChanges();
                }

                var users = (from u in entities.Users select u.Username).Distinct().ToList();

                Random random = new Random();

                for (int i = 0; i < 1000; i++)
                {
                    Favorite favorite = new Favorite()
                    {
                        UserId = users[random.Next(users.Count)],
                        RecipeId = random.Next(638100, 638250),
                        Rating = random.Next(1, 6)
                    };
                    entities.Favorites.Add(favorite);
                    entities.SaveChanges();
                }

                var query = from f in entities.Favorites
                            join u in entities.Users on f.UserId equals u.Username
                            select new { u.ID, f.RecipeId, f.Rating };

                query.Distinct().ToList().ForEach((x) =>
                {
                    DataRow row = table.NewRow();

                    row.SetField<float>("UserId", (float)Convert.ToDouble(x.ID));
                    row.SetField<float>("RecipeId", (float)x.RecipeId);
                    row.SetField<float>("Rating", (float)x.Rating);

                    table.Rows.Add(row);
                });


                ExportData.ToCSV(table, Path.Combine(Environment.CurrentDirectory, "Data", "train.csv"));

                MLContext context = new MLContext();


                IDataView trainDataView = context.Data.LoadFromTextFile<RecipeRating>(Path.Combine(Environment.CurrentDirectory, "Data", "train.csv"), hasHeader: true, separatorChar: ',');

                DataOperationsCatalog.TrainTestData dataSplit = context.Data.TrainTestSplit(trainDataView, testFraction: 0.2);
                IDataView trainData = dataSplit.TrainSet;
                IDataView testData = dataSplit.TestSet;

                IEstimator<ITransformer> estimator = context.Transforms.Conversion.MapValueToKey(outputColumnName: "UserIdEncoded", inputColumnName: "UserId")
                    .Append(context.Transforms.Conversion.MapValueToKey(outputColumnName: "RecipeIdEncoded", inputColumnName: "RecipeId"));

                var options = new MatrixFactorizationTrainer.Options
                {
                    MatrixColumnIndexColumnName = "UserIdEncoded",
                    MatrixRowIndexColumnName = "RecipeIdEncoded",
                    LabelColumnName = "Rating",
                    NumberOfIterations = 50,
                    NumberOfThreads = 10,
                    ApproximationRank = 500,
                };

                var trainerEstimator = estimator.Append(context.Recommendation().Trainers.MatrixFactorization(options));

                Console.WriteLine("------------ Antrenare ------------");
                ITransformer model = trainerEstimator.Fit(trainData);


                Console.WriteLine("------------ Testare ------------");
                var prediction = model.Transform(testData);

                var metrics = context.Regression.Evaluate(prediction, labelColumnName: "Rating", scoreColumnName: "Score");

                Console.WriteLine("Media distantelor : " + metrics.RootMeanSquaredError.ToString());
                Console.WriteLine("Coeficient determinare: " + metrics.RSquared.ToString());


                Console.WriteLine("------------ Generare recomandari ------------");
                var predictionEngine = context.Model.CreatePredictionEngine<RecipeRating, RecipeRatingPrediction>(model);


                RecipeRating testInput = new RecipeRating();

                entities.Database.ExecuteSqlCommand("TRUNCATE TABLE [Recommendations]");
                var UserIds = (from f in entities.Favorites join u in entities.Users on f.UserId equals u.Username 
                               select new { UserId = u.ID, Username = u.Username }).ToList().Distinct().
                               ToDictionary(x => x.UserId.ToString(), x => x.Username);
                var RecipeIds = (from r in entities.Favorites select r.RecipeId).ToList().Distinct();

                foreach (string user in UserIds.Keys)
                {
                    testInput.UserId = (float)Convert.ToDouble(user);
                    string recommendedRecipes = "";

                    foreach (var recipe in RecipeIds)
                    {
                        testInput.RecipeId = recipe;

                        var movieRatingPrediction = predictionEngine.Predict(testInput);

                        if (Math.Round(movieRatingPrediction.Score, 1) > 3.5)
                        {
                            recommendedRecipes += testInput.RecipeId + ",";
                        }
                    }

                    Recommendation recommendation = new Recommendation();

                    recommendation.UserId = UserIds[user.ToString()];
                    recommendation.Recommendations = string.IsNullOrEmpty(recommendedRecipes) ? "" :
                        recommendedRecipes.Remove(recommendedRecipes.Length - 1);
                    entities.Recommendations.Add(recommendation);

                    entities.SaveChanges();
                }

            }

            Console.WriteLine("Done! ^^");
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
