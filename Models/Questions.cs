using System;
using System.Collections.Generic;

namespace BlazorTEST.Models
{
    public partial class Questions
    {
        public Questions()
        {
            Answer = new HashSet<Answer>();
            ExamHistoryDetails = new HashSet<ExamHistoryDetails>();
        }

        public long QuestionId { get; set; }
        public long? ExamId { get; set; }
        public bool IsMultiChoice { get; set; }
        public string QuestionText { get; set; }

        public virtual Exams Exam { get; set; }
        public virtual ICollection<Answer> Answer { get; set; }
        public virtual ICollection<ExamHistoryDetails> ExamHistoryDetails { get; set; }
    }
}
