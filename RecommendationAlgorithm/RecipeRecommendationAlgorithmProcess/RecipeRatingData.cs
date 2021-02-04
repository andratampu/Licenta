using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML.Data;

namespace RecipeRecommendationAlgorithmProcess
{
    public class RecipeRating
    {
        [LoadColumn(0)]
        public float UserId;
        [LoadColumn(1)]
        public float RecipeId;
        [LoadColumn(2)]
        public float Rating;
    }

    public class RecipeRatingPrediction
    {
        public float Rating;
        public float Score;
    }
}
