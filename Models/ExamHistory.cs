using System;
using System.Collections.Generic;

namespace BlazorTEST.Models
{
    public partial class ExamHistory
    {
        public ExamHistory()
        {
            ExamHistoryDetails = new HashSet<ExamHistoryDetails>();
        }

        public long ExamHistoryId { get; set; }
        public long? ExamId { get; set; }
        public long? UserId { get; set; }
        public DateTime? TakenDate { get; set; }
        public double? Result { get; set; }

        public virtual Exams Exam { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<ExamHistoryDetails> ExamHistoryDetails { get; set; }
    }
}
