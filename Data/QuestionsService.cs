using BlazorTEST.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BlazorTEST.Classes.DataModel.DataModelRepository;

namespace BlazorTEST.Data
{

    public class QuestionsService
    {
        private Int64 examId; // handles examId for question form
        private Int64 userId;
        private List<ExamAnswer> examAnswers;
        private List<Question> questions;
        private DateTime startDateTime;
        int questionCount;
        int score;

        public void initQuestionService(Int64 examId, Int64 userId)
        {
            this.userId = userId;
            this.examId = examId;
            startDateTime = DateTime.Now;
            examAnswers = new List<ExamAnswer>();
            score = 0;
        }


        public void appendExamAnswer(ExamAnswer examAnswer)
        {
            examAnswers.Add(examAnswer);
        }

        public List<Question> getQuestions()
        {
            List<Question> returned = new List<Question>();

            using (var context = new ExamsDBContext())
            {
                var returnValue = from questList in context.Questions
                                  where questList.ExamId == examId
                                  select new Question
                                  {
                                      questionId = questList.QuestionId,
                                      isMultiChoice = questList.IsMultiChoice,
                                      questionText = questList.QuestionText
                                  };

                returned = returnValue.ToList();
                questionCount = returned.Count();
                return returned;
            }

        }

        public List<Anwser> getAnwsers(Int64 questionId)
        {
            List<Anwser> returned = new List<Anwser>();

            using (var context = new ExamsDBContext())
            {
                var returnValue = from anwserList in context.Answer
                                  where anwserList.QuestionId == questionId
                                  select new Anwser
                                  {
                                      answerId = anwserList.AnswerId,
                                      isProper = anwserList.IsProper ?? false,
                                      anwserText = anwserList.AnswerText,
                                      isMarked = false

                                  };

                returned = returnValue.ToList();

                return returned;
            }
        }


        public void calculateAnwser(List<Anwser> anwsers)
        {
            if(anwsers.Where(x => x.isMarked && x.isProper).Count() == anwsers.Where(x => x.isProper).Count() && anwsers.Count !=0)
            {
                score++;
            }
        }

        public int getQuestionCount()
        {
            return this.questionCount;
        }

        public float getScorePercent()
        {
            return (float)score / (float)this.getQuestionCount()*100;
        }

        public int getScore()
        {
            return score;
        }

        public void finalizeExam()
        {
            ExamHistory exam = new ExamHistory();

            exam.ExamId = examId;
            exam.UserId = userId;
            exam.TakenDate = startDateTime;
            exam.Result = this.getScorePercent() / 100;

            using (var context = new ExamsDBContext())
            {
                context.ExamHistory.Add(exam);
                context.SaveChanges();
            }
        }
    }

}
