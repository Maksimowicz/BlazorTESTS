using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Created for avoid of using EF classes outside of services
/// </summary>
namespace BlazorTEST.Classes.DataModel
{
    public class DataModelRepository
    {
        /// <summary>
        /// Client side representation of PassedExams
        /// </summary>
        public class ExamsPassed
        {
            public Int64 examId { get; set; }
            public string Name { get; set; }
            public float Percent { get; set; }
            public Boolean Passed { get; set; }
        }

        /// <summary>
        /// Exams created by user
        /// </summary>
        public class ExamsCreated
        {
            public string ExamTitle { get; set; }
            public Int64 ExamId { get; set; }
            public Int64 ExamTakenCount { get; set; }
        }

        /// <summary>
        /// Record from list page with all exams
        /// </summary>
        public class ExamListPageItem
        {
            public string ExamTitle { get; set; }
            public Int64 ExamId { get; set; }
            public string CreatedBy { get; set; }
            public Int64 ExamQuestionCount { get; set; }
            public Int64 ExamTakenCount { get; set; }
            public string ExamDescription { get; set; }


        }

        /// <summary>
        /// Graph record
        /// </summary>
        public class DateGraphPoint
        {
            public DateTime date { get; set; }
            public int takenCount { get; set; }

            public DateGraphPoint(DateTime date, int takenCount)
            {
                this.date = date;
                this.takenCount = takenCount;
            }
        }

        /// <summary>
        /// Statistics of chosen exam
        /// </summary>
        public class ExamStatistics
        {
            public float passedPercentage { get; set; }
            public DateGraphPoint[] takenGraph { get; set; }
            public DateTime firstTimeFlawless { get; set; }

        }

        public class ExamAnswer
        {
            public Int64 questionId { get; set; }
            public int[] anwsers { get; set; }

            public Boolean score { get; set; }
        }

        public class Question
        {
            public Int64 questionId { get; set; }
            public Boolean isMultiChoice { get; set; }
            public string questionText { get; set; }
        }

        public class Anwser
        {
            public Int64 answerId { get; set; }
            public Boolean isProper { get; set; }
            public string anwserText { get; set; }

            public bool isMarked { get; set; }
            public Int64 anwserIdMock { get; set; }
        }
    }
}
