using BlazorTEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BlazorTEST.Data
{
    public class CreateExamService
    {
        Exams examHeader;
        List<Questions> questions;
        List<Answer> anwsers;

        bool headerCreated;


        public void initializeExam(Int64 userId, string title, string description)
        {
            questions = new List<Questions>();
            anwsers = new List<Answer>();

            examHeader = new Exams();
            examHeader.UserId = userId;
            examHeader.Title = title;
            examHeader.Decription = description;

            headerCreated = true;

            using (var context = new ExamsDBContext())
            {
                context.Exams.Add(examHeader);
                context.SaveChanges();

            };
        }

        public void createQuestion(bool isMultiChoice, string questionTekst)
        {
            Questions questionsHelepr = new Questions();

            questionsHelepr.IsMultiChoice = isMultiChoice;
            questionsHelepr.QuestionText = questionTekst;
            questionsHelepr.ExamId = examHeader.ExamId;

            using (var context = new ExamsDBContext())
            {
                context.Questions.Add(questionsHelepr);
                context.SaveChanges();
            };

            questions.Add(questionsHelepr);

        }

        public void addAnswerToQuestion(string anwserText, bool isProper)
        {
            Answer answerHelper = new Answer();
            answerHelper.AnswerText = anwserText;
            answerHelper.IsProper = isProper;
            answerHelper.QuestionId = questions[questions.Count - 1].QuestionId;

            using (var context = new ExamsDBContext())
            {
                context.Answer.Add(answerHelper);
                context.SaveChanges();
            };

            anwsers.Add(answerHelper);
        }

        public bool checkIfTitleIsAvail(string title)
        {
            using (var context = new ExamsDBContext())
            {
                var returnValue = context.Exams.Any(x => x.Title == title);

                return !returnValue;
            };
        }
    }
}
