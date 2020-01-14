using System;
using System.Collections.Generic;

namespace BlazorTEST.Models
{
    public partial class PassedPercentage
    {
        public long? ExamId { get; set; }
        public int? TotalTaken { get; set; }
        public int? TotalPassed { get; set; }
        public double? PassedPercentage1 { get; set; }
    }
}
