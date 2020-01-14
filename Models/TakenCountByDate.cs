using System;
using System.Collections.Generic;

namespace BlazorTEST.Models
{
    public partial class TakenCountByDate
    {
        public long? ExamId { get; set; }
        public int? TakenCount { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
