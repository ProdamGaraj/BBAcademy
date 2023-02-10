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
                    Login = "azaza",
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
                    Description = "Example Course",
                    Price = 505,
                    DurationHours = 10,
                    CertificateTemplate = new CertificateTemplate()
                    {
                        MediaPath = "/templates/template1.pdf",
                        CreatedAt = DateTime.UtcNow
                    },
                    Lessons = new List<Lesson>()
                    {
                        new Lesson()
                        {
                            Content = "Lesson PHOTO",
                            CreatedAt = DateTime.UtcNow,
                            LessonContentType = LessonContentType.Photo,
                            MediaContentPath = "/img/photo.png"
                        },

                        new Lesson()
                        {
                            Content = "Lesson VIDEO",
                            CreatedAt = DateTime.UtcNow,
                            LessonContentType = LessonContentType.Video,
                            MediaContentPath = "/img/video.mp4"
                        },

                        new Lesson()
                        {
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
                        CreatedAt = DateTime.UtcNow,
                        MinimumPassingGrade = 99,
                        Description = "Example EXAM",
                        Questions = new List<Question>()
                        {
                            new Question()
                            {
                                Content = "question 1",
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
                                Content = "question 2",
                                MediaPath = "/img/question2.png",
                                CreatedAt = DateTime.UtcNow,
                                QuestionType = QuestionType.ManyAnswers,
                                AnswerOptions = new List<AnswerOption>()
                                {
                                    new AnswerOption()
                                    {
                                        Title = "Answer Option opt1 correct",
                                        IsCorrect = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                    new AnswerOption()
                                    {
                                        Title = "Answer Option opt2 correct",
                                        IsCorrect = true,
                                        CreatedAt = DateTime.UtcNow,
                                        Weight = 100
                                    },
                                    new AnswerOption()
                                    {
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

            serviceScope.ServiceProvider.GetRequiredService<ILogger<BilimContext>>()
                .LogCritical("DATABASE SEEDED");
        }
    }
}