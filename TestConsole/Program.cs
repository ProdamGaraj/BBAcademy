﻿// See https://aka.ms/new-console-template for more information
using Backend.Models;
using Backend.Models.Enum;
using Backend.Services;
using Backend.Services.Repository;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

Console.WriteLine("creating the Exam");
ExamRepository er = new ExamRepository();
Exam Exam = new Exam("20 minutes", "Exam for banking", new List<Question>());
await er.Add(Exam);
Exam = await er.Get(Exam.Id);
Console.WriteLine(JsonConvert.SerializeObject(Exam));
Console.WriteLine("Changing description to 2 hours");
Exam.Description = "2 hours";
await er.Update(Exam);
Exam = await er.Get(Exam.Id);
Console.WriteLine(JsonConvert.SerializeObject(Exam));
Console.WriteLine("Marking as deleted");
//await er.MarkAsDeleted(Exam);
Exam = await er.Get(Exam.Id);
Console.WriteLine(JsonConvert.SerializeObject(Exam));
Console.WriteLine("Creating the lesson");
LessonRepository lr = new LessonRepository();
CourseRepository cor = new CourseRepository();

Lesson lesson = new Lesson(LessonType.Photo,"Photo", "Photo of a dog", "C://");
await lr.Add(lesson);
lesson = await lr.Get(lesson.Id);
Console.WriteLine(JsonConvert.SerializeObject(lesson));
lesson.Description = "New description";
Console.WriteLine("Changing description to new description");
await lr.Update(lesson);
lesson = await lr.Get(lesson.Id);
Console.WriteLine(JsonConvert.SerializeObject(lesson));
Console.WriteLine("Marking as deleted");
//await lr.MarkAsDeleted(lesson);
lesson = await lr.Get(lesson.Id);
Console.WriteLine(JsonConvert.SerializeObject(lesson));
Console.WriteLine("Creating the answer");
AnswerRepository ar = new AnswerRepository();
Answer Answer = new Answer("Sun is yellow", false,110);
await ar.Add(Answer);
Answer = await ar.Get(Answer.Id);
Console.WriteLine(JsonConvert.SerializeObject(Answer));
Console.WriteLine("Changing cost to 10");
Answer.Cost = 10;
await ar.Update(Answer);
Answer = await ar.Get(Answer.Id);
Console.WriteLine(JsonConvert.SerializeObject(Answer));
Console.WriteLine("Marking as deleted");
//await ar.MarkAsDeleted(Answer);
Answer = await ar.Get(Answer.Id);
Console.WriteLine(JsonConvert.SerializeObject(Answer));
Console.WriteLine("Creating the certificate");
UserRepository ur = new UserRepository();
User User = new User("Maximus", DateTime.Now, DateTime.Now, "Powery", "Poiwer", "password", "login", "C://", true, "power@pow.ru", "Telme", "junior manager", "", null, null, null, "not much to say");
await ur.Add(User);
CertificateRepository cr = new CertificateRepository();
Certificate Certificate = new Certificate(User.Id,"Some media templatepath",2, new List<Course>());
bool flag=await cr.Add(Certificate);
Certificate = await cr.Get(Certificate.Id);
Console.WriteLine(JsonConvert.SerializeObject(Certificate));
Console.WriteLine("Changing courseid to 1");
Certificate.CourseId = 1;
await cr.Update(Certificate);
Certificate = await cr.Get(Certificate.Id);
Console.WriteLine(JsonConvert.SerializeObject(Certificate));
Console.WriteLine("Marking as deleted");
//await cr.MarkAsDeleted(Certificate);
Certificate = await cr.Get(Certificate.Id);
Console.WriteLine(JsonConvert.SerializeObject(Certificate));
await ur.MarkAsDeleted(User);
Console.WriteLine("creating the Course");
Course Course = new Course(null,"20 minutes", "course for banking",CourseType.Passed,null,await er.Get(2));
await cor.Add(Course);
Course = await cor.Get(Course.Id);
Console.WriteLine(JsonConvert.SerializeObject(Course));
Console.WriteLine("Changing duration to 2 hours");
Course.Duration = "2 hours";
await cor.Update(Course);
Course = await cor.Get(Course.Id);
Console.WriteLine(JsonConvert.SerializeObject(Course));
Console.WriteLine("Marking as deleted");
//await cor.MarkAsDeleted(Course);
Course = await cor.Get(Course.Id);
Console.WriteLine(JsonConvert.SerializeObject(Course));
Console.WriteLine("creating the Question");
QuestionRepository qr = new QuestionRepository();
Question Question = new Question("Goodbyes",0,null);
await qr.Add(Question);
Question = await qr.Get(Question.Id);
Console.WriteLine(JsonConvert.SerializeObject(Question));
Console.WriteLine("Changing content to famous person speaking");
Question.Content = "famous person speaking";
await qr.Update(Question);
Question = await qr.Get(Question.Id);
Console.WriteLine(JsonConvert.SerializeObject(Question));
Console.WriteLine("Marking as deleted");
//await qr.MarkAsDeleted(Question);
Question = await qr.Get(Question.Id);
Console.WriteLine(JsonConvert.SerializeObject(Question));
Console.WriteLine("creating the User");
User = new User("Maximus",DateTime.Now,DateTime.Now,"Powery","Poiwer","password","login","C://",true,"power@pow.ru","Telme","junior manager","",null,null,null,"not much to say");
await ur.Add(User);
User = await ur.Get(User.Id);
Console.WriteLine(JsonConvert.SerializeObject(User));
Console.WriteLine("Changing name to Tima");
User.Name = "Tima";
await ur.Update(User);
User = await ur.Get(User.Id);
Console.WriteLine(JsonConvert.SerializeObject(User));
Console.WriteLine("Marking as deleted");
//await ur.MarkAsDeleted(User);
User = await ur.Get(User.Id);
List<long> ids = new List<long>();
List<Question> questions = new List<Question>();
Question = new Question("Goodbyes", QuestionType.OneAnswer, null);
await qr.Add(Question);
ids.Add(Question.Id);
questions.Add(Question);
Question = new Question("Goodbyes", QuestionType.TextAnswer, null);
await qr.Add(Question);
ids.Add(Question.Id);
questions.Add(Question);
Question = new Question("Goodbyes", QuestionType.ManyAnswers, null);
await qr.Add(Question);
ids.Add(Question.Id);
questions.Add(Question);
Question = new Question("Goodbyes", QuestionType.OneAnswer, null);
await qr.Add(Question);
ids.Add(Question.Id);
questions.Add(Question);
Question = new Question("Goodbyes", QuestionType.TextAnswer, null);
await qr.Add(Question);
ids.Add(Question.Id);
questions.Add(Question);
Question = new Question("Goodbyes", QuestionType.ManyAnswers, null);
await qr.Add(Question);
ids.Add(Question.Id);
questions.Add(Question);
Question = new Question("Goodbyes", QuestionType.ManyAnswers, null);
await qr.Add(Question);
ids.Add(Question.Id);
questions.Add(Question);
Console.WriteLine(JsonConvert.SerializeObject(User));
Console.WriteLine("Create exam with questions by ids");
ExamService ec = new ExamService();
Exam = (await ec.CreateExamWithId("20 minutes", "Exam for banking",ids)).Data;
await er.Add(Exam);
Exam = await er.Get(Exam.Id);
Console.WriteLine(JsonConvert.SerializeObject(Exam));
Console.WriteLine("Create exam with all types with pairs type:amount - 0:1 1:2 2:4");
Dictionary<QuestionType, int> keyValues = new Dictionary<QuestionType, int>();
keyValues.Add(QuestionType.OneAnswer,1); 
keyValues.Add(QuestionType.TextAnswer,2);
keyValues.Add(QuestionType.ManyAnswers,4); 
Exam = (await ec.CreateExamWithType("20 minutes", "Exam for banking", keyValues)).Data;
await er.Add(Exam);
Exam = await er.Get(Exam.Id);
Console.WriteLine(JsonConvert.SerializeObject(Exam));
foreach (Question entity in questions)
{
    //await qr.MarkAsDeleted(entity);
}