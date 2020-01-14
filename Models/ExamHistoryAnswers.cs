using System;
using System.Collections.Generic;

namespace BlazorTEST.Models
{
    public partial class ExamHistoryAnswers
    {
        public long? ExamLineId { get; set; }
        public long? AnswerId { get; set; }
        public long ExamAnswerId { get; set; }

        public virtual Answer Answer { get; set; }
        public virtual ExamHistoryDetails ExamLine { get; set; }
    }
}
