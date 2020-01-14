using System;
using System.Collections.Generic;

namespace BlazorTEST.Models
{
    public partial class Users
    {
        public Users()
        {
            ExamHistory = new HashSet<ExamHistory>();
            Exams = new HashSet<Exams>();
        }

        public long UserId { get; set; }
        public string UserLogin { get; set; }
        public string UserPassword { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string Email { get; set; }

        public virtual ICollection<ExamHistory> ExamHistory { get; set; }
        public virtual ICollection<Exams> Exams { get; set; }
    }
}
