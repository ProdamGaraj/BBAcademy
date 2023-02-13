using Infrastructure.Data;
using Infrastructure.Helpers;
using Infrastructure.Models;
using Infrastructure.Models.Enum;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure;

public static class DI
{
    public static IServiceCollection AddDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Bilim");

        services.AddDbContext<BilimContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped(typeof(IRepository<>), typeof(RepositoryBase<>));

        return services;
    }

    public static async Task MigrateDb(this IServiceProvider provider)
    {
        Console.WriteLine("Migrating Db");
        using var serviceScope = provider.CreateScope();
        var context = serviceScope.ServiceProvider.GetRequiredService<BilimContext>();
        await serviceScope.ServiceProvider.GetRequiredService<BilimContext>()
            .Database.MigrateAsync();

        if (!await context.Users.AnyAsync())
        {
            context.Users.Add(
                new User()
                {
                    Login = "azaza1",
                    PasswordHash = PasswordHasher.GetPasswordHash("azaza123"),
                    Email = "azaza@zaza.aza",
                    Organisation = "Gryadka",
                    Surname = "Percov",
                    FirstName = "Perec",
                    MiddleName = "Percovich",
                    CreatedAt = DateTime.UtcNow,
                    JobTitle = "Enjoyer",
                    Role = UserRole.User,
                    Sex = true,
                    AboutMe = "Average Perec",
                    PhotoPath = "/img/perec-percovich.png"
                }
            );

            context.Courses.Add(
                new Course()
                {
                    Title = "This is Example Course Title",
                    Description = "This is some Example Course long description: Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                    Price = 505,
                    DurationHours = 10,
                    CertificateTemplate = new CertificateTemplate()
                    {
                        MediaPath = "/mounted/templates/cert.pdf",
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
                            MediaContentPath = "/img/video.mp4"
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
                        MinimumPassingGrade = 8, // total 10
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
                                        Weight = 6
                                    },
                                    new AnswerOption()
                                    {
                                        Title = "Answer Option INCORRECT",
                                        IsCorrect = false,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 0
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
                                        Weight = 2
                                    },
                                    new AnswerOption()
                                    {
                                        Order = 2,
                                        Title = "Answer Option opt2 correct",
                                        IsCorrect = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 2
                                    },
                                    new AnswerOption()
                                    {
                                        Order = 3,
                                        Title = "Answer Option opt3 incorrect",
                                        IsCorrect = false,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 0
                                    },
                                }
                            }
                        }
                    }
                }
            );

            await context.SaveChangesAsync();

            serviceScope.ServiceProvider.GetRequiredService<ILogger<BilimContext>>()
                .LogCritical("DATABASE SEEDED");
        }
    }
}