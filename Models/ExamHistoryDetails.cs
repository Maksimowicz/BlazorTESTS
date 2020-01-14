using System;
using System.Collections.Generic;

namespace BlazorTEST.Models
{
    public partial class ExamHistoryDetails
    {
        public ExamHistoryDetails()
        {
            ExamHistoryAnswers = new HashSet<ExamHistoryAnswers>();
        }

        public long ExamLineId { get; set; }
        public long? ExamHistoryId { get; set; }
        public long? QuestionId { get; set; }

        public virtual ExamHistory ExamHistory { get; set; }
        public virtual Questions Question { get; set; }
        public virtual ICollection<ExamHistoryAnswers> ExamHistoryAnswers { get; set; }
    }
}
