using BlazorTEST.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BlazorTEST.Classes.DataModel.DataModelRepository;


namespace BlazorTEST.Data
{
   

    public class ExamsService
    {
        public async Task<List<ExamsPassed>> getTakenExams(Int64 userId)
        {
            List<ExamsPassed> returned = null;
            using (var context = new ExamsDBContext())
            {
                var returnValue = from exams in context.Exams
                                  join examHistory in context.ExamHistory on exams.ExamId equals examHistory.ExamId
                                  where examHistory.UserId == userId
                                  select new ExamsPassed
                                  {
                                      examId = exams.ExamId,
                                      Name = exams.Title,
                                      Percent = examHistory.Result == null ? 0 : (float)examHistory.Result,
                                      Passed = examHistory.Result > 0.50 ? true : false
                                  };

                returned = await returnValue.ToListAsync();
                return returned;

            }
            return returned;

        }

        public async Task<List<ExamsCreated>> getCreatedExams(Int64 userId)
        {
            List<ExamsCreated> created = null;
            using (var context = new ExamsDBContext())
            {
                var returnValue = from exams in context.Exams
                                  where exams.UserId == userId
                                  select new ExamsCreated
                                  {
                                      ExamId = exams.ExamId,
                                      ExamTitle = exams.Title,
                                      ExamTakenCount = exams.ExamHistory.Count
                                  };

                created = await returnValue.ToListAsync();
                return created;
            }
            return created;
        }

        public async Task<int> getTakenExamsCount(Int64 userId)
        {
            int returned = 0;

            using (var context = new ExamsDBContext())
            {
                var returnValue = from exams in context.Exams
                                  join examHistory in context.ExamHistory on exams.ExamId equals examHistory.ExamId
                                  where examHistory.UserId == userId
                                  select exams;

                returned = await returnValue.CountAsync();
                return returned;
            }

            return returned;
        }

        public async Task<int> getCreatedExamsCount(Int64 userId)
        {
            int returned = 0;

            using (var context = new ExamsDBContext())
            {
                var returnValue = from exams in context.Exams
                                  where exams.UserId == userId
                                  select exams;

                returned = await returnValue.CountAsync();

                return returned;
            }

            return returned;
        }

        public async Task<List<ExamListPageItem>> getExamsList(int count)
        {
            List<ExamListPageItem> returned = null;
            using (var context = new ExamsDBContext())
            {
                var returnValue = from exams in context.Exams
                                  join users in context.Users on exams.UserId equals users.UserId
                                  select new ExamListPageItem
                                  {
                                      ExamTitle = exams.Title,
                                      CreatedBy = users.UserLogin,
                                      ExamId = exams.ExamId,
                                      ExamQuestionCount = exams.Questions.Count,
                                      ExamTakenCount = exams.ExamHistory.Count
                                  };
                returned = await returnValue.ToListAsync();

                return returned;
            }

            return returned;
        }

        public async Task<int> getListsCount()
        {
            using (var context = new ExamsDBContext())
            {
                var returnValue = from exams in context.Exams
                                  select exams;

                return await returnValue.CountAsync();
            }
            return 0;
        }


        public  ExamListPageItem getSpecifiedExam(Int64 examId)
        {
            
            using (var context = new ExamsDBContext())
            {
                var returnValue = from exams in context.Exams
                                  join users in context.Users on exams.UserId equals users.UserId
                                  where exams.ExamId == examId
                                  select new ExamListPageItem
                                  {
                                      ExamTitle = exams.Title,
                                      ExamId = exams.ExamId,
                                      ExamDescription = exams.Decription ?? "Brak",
                                      ExamQuestionCount = exams.Questions.Count,
                                      ExamTakenCount = exams.ExamHistory.Count,
                                      CreatedBy = users.UserLogin
                                  };

                var examListSingle = returnValue.ToList();
                return examListSingle[0];

            }

            return new ExamListPageItem();
        }

        public ExamStatistics getSpecifiedExamStatistics(Int64 examId)
        {
           
            ExamStatistics examStatistics = new ExamStatistics();
            DateGraphPoint[] dateGraphPoints = null;

            using(var context = new ExamsDBContext())
            {
                var basicStatsQuery = from examPercentage in context.PassedPercentage
                                  join examFlawless in context.FlawlessView
                                  on examPercentage.ExamId equals examFlawless.ExamId into tmpMap
                                  from examFlawless in tmpMap.DefaultIfEmpty()
                                  where examPercentage.ExamId == examId
                                  select new
                                  {
                                      flawlessDate = examFlawless.FlawlessDate,
                                      passedPercentage = examPercentage.PassedPercentage1
                                  };

                var graphStatsQuery = from graphStats in context.TakenCountByDate
                                      where graphStats.ExamId == examId
                                      select graphStats;

                
               

                var basicStatsList =  basicStatsQuery.ToList();
                var graphStatsList =  graphStatsQuery.ToList();
                if (basicStatsList.Count > 0)
                {
                    examStatistics.firstTimeFlawless = basicStatsList[0].flawlessDate ?? DateTime.MinValue;
                    examStatistics.passedPercentage = (float)(basicStatsList[0].passedPercentage ?? 0);
                }

         

                dateGraphPoints = new DateGraphPoint[graphStatsQuery.Count()];

                for(int i = 0; i < graphStatsList.Count; ++i)
                {
                    dateGraphPoints[i] = new DateGraphPoint(graphStatsList[i].DateCreated ?? DateTime.MinValue,
                                                                graphStatsList[i].TakenCount ?? 0);
                }

                examStatistics.takenGraph = dateGraphPoints;

                return examStatistics;
            }
        }





    }
}
