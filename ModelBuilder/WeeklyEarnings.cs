using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ML.Data;

namespace ModelBuilder
{
   public class WeeklyEarnings
    {
        [LoadColumn(0)]
        public int Date { get; set; }

        [LoadColumn(1)]
        public string Geography { get; set; }

        [LoadColumn(3)]
        public string Industry { get; set; }

        [LoadColumn(4)]
        public float AverageWeeklyEarnings { get; set; }
    }

    public class WeeklyEarningsPrediction
    {
        [ColumnName("Score")]
        public float AverageWeeklyEarnings { get; set; }

    }
}
