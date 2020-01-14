using System;
using System.Collections.Generic;

namespace BlazorTEST.Models
{
    public partial class Answer
    {
        public Answer()
        {
            ExamHistoryAnswers = new HashSet<ExamHistoryAnswers>();
        }

        public long AnswerId { get; set; }
        public long? QuestionId { get; set; }
        public string AnswerText { get; set; }
        public bool? IsProper { get; set; }

        public virtual Questions Question { get; set; }
        public virtual ICollection<ExamHistoryAnswers> ExamHistoryAnswers { get; set; }
    }
}
