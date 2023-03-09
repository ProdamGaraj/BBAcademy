using Infrastructure.Data;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Models.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public class Seeder
{
    public static async Task Seed(BilimContext context, ILogger<BilimContext> logger)
    {
        if (!await context.Users.AnyAsync())
        {
            context.Users.Add(
                new User()
                {
                    Login = "admin",
                    PasswordHash = PasswordHasher.GetPasswordHash("password"),
                    Email = "admin@adm.adm",
                    Organisation = "BilimBank",
                    Surname = "Adminof",
                    FirstName = "Admin",
                    MiddleName = "Adminoffich",
                    Phone = "+79611056161",
                    CreatedAt = DateTime.UtcNow,
                    JobTitle = "Tester",
                    Role = UserRole.User,
                    Sex = true,
                    AboutMe = "Some text about big and great admin, who once sold the world.",
                    PhotoPath = "/img/perec-percovich.png"
                }
            );

            context.Courses.Add(
                new Course()
                {
                    Title = "Course 1",
                    Description = "This is some Example Course long description: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Price = 500,
                    DurationHours = 10,
                    CertificateTemplate = new CertificateTemplate()
                    {
                        MediaPath = "/mounted/templates/template1.pdf",
                        CreatedAt = DateTime.UtcNow
                    },
                    Lessons = new List<Lesson>()
                    {
                        new Lesson()
                        {
                            Title = "ex LESSON1",
                            Order = 1,
                            Content = "Lesson PHOTO",
                            CreatedAt = DateTime.UtcNow,
                            LessonContentType = LessonContentType.Photo,
                            MediaContentPath = "/img/photo.png"
                        },

                        new Lesson()
                        {
                            Title = "ex LESSON2",
                            Order = 2,
                            Content = "Lesson VIDEO",
                            CreatedAt = DateTime.UtcNow,
                            LessonContentType = LessonContentType.Video,
                            MediaContentPath = "/vid/6.mp4"
                        },

                        new Lesson()
                        {
                            Title = "ex LESSON3",
                            Order = 3,
                            Content = "Lesson TEXT",
                            CreatedAt = DateTime.UtcNow,
                            LessonContentType = LessonContentType.Text,
                            MediaContentPath = null
                        }
                    },
                    MediaPath = "/img/Shared/course_guy.jpg",
                    CreatedAt = DateTime.UtcNow,
                    Exam = new Exam()
                    {
                        Title = "Example EXAM",
                        CreatedAt = DateTime.UtcNow,
                        MinimumPassingGrade = 99,
                        Questions = new List<Question>()
                        {
                            new Question()
                            {
                                Title = "question 1",
                                Order = 1,
                                MediaPath = "/img/question1.png",
                                CreatedAt = DateTime.UtcNow,
                                QuestionType = QuestionType.OneAnswer,
                                AnswerOptions = new List<AnswerOption>()
                                {
                                    new AnswerOption()
                                    {
                                        Title = "Answer Option CORRECT",
                                        IsCorrect = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                    new AnswerOption()
                                    {
                                        Title = "Answer Option INCORRECT",
                                        IsCorrect = false,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                }
                            },
                            new Question()
                            {
                                Title = "question 2",
                                Order = 2,
                                MediaPath = "/img/question2.png",
                                CreatedAt = DateTime.UtcNow,
                                QuestionType = QuestionType.ManyAnswers,
                                AnswerOptions = new List<AnswerOption>()
                                {
                                    new AnswerOption()
                                    {
                                        Order = 1,
                                        Title = "Answer Option opt1 correct",
                                        IsCorrect = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                    new AnswerOption()
                                    {
                                        Order = 2,
                                        Title = "Answer Option opt2 correct",
                                        IsCorrect = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                    new AnswerOption()
                                    {
                                        Order = 3,
                                        Title = "Answer Option opt3 incorrect",
                                        IsCorrect = false,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                }
                            }
                        }
                    }
                }
            );

            context.Courses.Add(
                new Course()
                {
                    Title = "Course 2",
                    Description = "This is some Example Course long description: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Price = 500,
                    DurationHours = 10,
                    CertificateTemplate = new CertificateTemplate()
                    {
                        MediaPath = "/mounted/templates/template1.pdf",
                        CreatedAt = DateTime.UtcNow
                    },
                    Lessons = new List<Lesson>()
                    {
                        new Lesson()
                        {
                            Title = "LESSON1",
                            Order = 1,
                            Content = "Lesson PHOTO",
                            CreatedAt = DateTime.UtcNow,
                            LessonContentType = LessonContentType.Photo,
                            MediaContentPath = "/img/photo.png"
                        },

                        new Lesson()
                        {
                            Title = "LESSON2",
                            Order = 2,
                            Content = "Lesson VIDEO",
                            CreatedAt = DateTime.UtcNow,
                            LessonContentType = LessonContentType.Video,
                            MediaContentPath = "/vid/6.mp4"
                        },

                        new Lesson()
                        {
                            Title = "LESSON3",
                            Order = 3,
                            Content = "Lesson TEXT",
                            CreatedAt = DateTime.UtcNow,
                            LessonContentType = LessonContentType.Text,
                            MediaContentPath = null
                        }
                    },
                    MediaPath = "/img/Shared/course_guy.jpg",
                    CreatedAt = DateTime.UtcNow,
                    Exam = new Exam()
                    {
                        Title = "Example EXAM",
                        CreatedAt = DateTime.UtcNow,
                        MinimumPassingGrade = 99,
                        Questions = new List<Question>()
                        {
                            new Question()
                            {
                                Title = "question 1",
                                Order = 1,
                                MediaPath = "/img/question1.png",
                                CreatedAt = DateTime.UtcNow,
                                QuestionType = QuestionType.OneAnswer,
                                AnswerOptions = new List<AnswerOption>()
                                {
                                    new AnswerOption()
                                    {
                                        Title = "Answer Option CORRECT",
                                        IsCorrect = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                    new AnswerOption()
                                    {
                                        Title = "Answer Option INCORRECT",
                                        IsCorrect = false,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                }
                            },
                            new Question()
                            {
                                Title = "question 2",
                                Order = 2,
                                MediaPath = "/img/question2.png",
                                CreatedAt = DateTime.UtcNow,
                                QuestionType = QuestionType.ManyAnswers,
                                AnswerOptions = new List<AnswerOption>()
                                {
                                    new AnswerOption()
                                    {
                                        Order = 1,
                                        Title = "Answer Option opt1 correct",
                                        IsCorrect = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                    new AnswerOption()
                                    {
                                        Order = 2,
                                        Title = "Answer Option opt2 correct",
                                        IsCorrect = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                    new AnswerOption()
                                    {
                                        Order = 3,
                                        Title = "Answer Option opt3 incorrect",
                                        IsCorrect = false,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                }
                            }
                        }
                    }
                }
            );
            context.Courses.Add(
                new Course()
                {
                    Title = "Course 3",
                    Description = "3rd seeded course Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Price = 500,
                    DurationHours = 10,
                    CertificateTemplate = new CertificateTemplate()
                    {
                        MediaPath = "/mounted/templates/template1.pdf",
                        CreatedAt = DateTime.UtcNow
                    },
                    Lessons = new List<Lesson>()
                    {
                        new Lesson()
                        {
                            Title = "LESSON1",
                            Order = 1,
                            Content = "Lesson PHOTO",
                            CreatedAt = DateTime.UtcNow,
                            LessonContentType = LessonContentType.Photo,
                            MediaContentPath = "/img/photo.png"
                        },

                        new Lesson()
                        {
                            Title = "LESSON2",
                            Order = 2,
                            Content = "Lesson VIDEO",
                            CreatedAt = DateTime.UtcNow,
                            LessonContentType = LessonContentType.Video,
                            MediaContentPath = "/vid/6.mp4"
                        },

                        new Lesson()
                        {
                            Title = "LESSON3",
                            Order = 3,
                            Content = "Lesson TEXT",
                            CreatedAt = DateTime.UtcNow,
                            LessonContentType = LessonContentType.Text,
                            MediaContentPath = null
                        }
                    },
                    MediaPath = "/img/Shared/course_guy.jpg",
                    CreatedAt = DateTime.UtcNow,
                    Exam = new Exam()
                    {
                        Title = "Example EXAM",
                        CreatedAt = DateTime.UtcNow,
                        MinimumPassingGrade = 99,
                        Questions = new List<Question>()
                        {
                            new Question()
                            {
                                Title = "question 1",
                                Order = 1,
                                MediaPath = "/img/question1.png",
                                CreatedAt = DateTime.UtcNow,
                                QuestionType = QuestionType.OneAnswer,
                                AnswerOptions = new List<AnswerOption>()
                                {
                                    new AnswerOption()
                                    {
                                        Title = "Answer Option CORRECT",
                                        IsCorrect = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                    new AnswerOption()
                                    {
                                        Title = "Answer Option INCORRECT",
                                        IsCorrect = false,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                }
                            },
                            new Question()
                            {
                                Title = "question 2",
                                Order = 2,
                                MediaPath = "/img/question2.png",
                                CreatedAt = DateTime.UtcNow,
                                QuestionType = QuestionType.ManyAnswers,
                                AnswerOptions = new List<AnswerOption>()
                                {
                                    new AnswerOption()
                                    {
                                        Order = 1,
                                        Title = "Answer Option opt1 correct",
                                        IsCorrect = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                    new AnswerOption()
                                    {
                                        Order = 2,
                                        Title = "Answer Option opt2 correct",
                                        IsCorrect = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                    new AnswerOption()
                                    {
                                        Order = 3,
                                        Title = "Answer Option opt3 incorrect",
                                        IsCorrect = false,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                }
                            }
                        }
                    }
                }
            );

            await context.SaveChangesAsync();
            logger.LogCritical("DATABASE SEEDED");
        }
    }
}