using System;
using System.Collections.Generic;

namespace BlazorTEST.Models
{
    public partial class Exams
    {
        public Exams()
        {
            ExamHistory = new HashSet<ExamHistory>();
            Questions = new HashSet<Questions>();
        }

        public long ExamId { get; set; }
        public long? UserId { get; set; }
        public string Title { get; set; }
        public string Decription { get; set; }

        public virtual Users User { get; set; }
        public virtual ICollection<ExamHistory> ExamHistory { get; set; }
        public virtual ICollection<Questions> Questions { get; set; }
    }
}
