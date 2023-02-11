using System.Collections;
using AutoMapper;
using BLL.Models.GetExamForTesting;
using BLL.Models.SaveCourseExamResults;
using BLL.Services.Interfaces;
using Infrastructure.Common;
using Infrastructure.Models;
using Infrastructure.Models.Enum;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BLL.ExamService;

public class ExamService : IExamService
{
    private readonly IRepository<Exam> _examRepository;
    private readonly ICourseProgressService _courseProgressService;
    private readonly ILogger<ExamService> _logger;

    public ExamService(IRepository<Exam> examRepository, ILogger<ExamService> logger, ICourseProgressService courseProgressService)
    {
        _examRepository = examRepository;
        _logger = logger;
        _courseProgressService = courseProgressService;
    }

    public async Task<bool> SaveCourseExamResults(long userId, SaveCourseExamResultsDto dto)
    {
        var exam = await _examRepository.GetAll()
            .Where(e => e.Course.Id == dto.CourseId)
            .Include(e => e.Questions.OrderBy(q => q.Order))
            .ThenInclude(q => q.AnswerOptions.OrderBy(a => a.Order))
            .FirstOrDefaultAsync();

        if (exam is null)
        {
            throw new BusinessException("Course Was Not Found");
        }

        if (dto.Questions.Count != exam.Questions.Count)
        {
            throw new BusinessException("Questions count Mismatch");
        }

        var totalWeight = 0;

        foreach (var (dQuestion, eQuestion) in dto.Questions.Zip(exam.Questions))
        {
            switch (eQuestion.QuestionType)
            {
                case QuestionType.OneAnswer:
                {
                    if (dQuestion.SelectedAnswerIds.Count > 1)
                    {
                        // unreal case
                        throw new BusinessException("Для вопроса с 1 ответом было получено больше 1 ответа");
                    }
                    else if (dQuestion.SelectedAnswerIds.Count == 0)
                    {
                        throw new BusinessException("Для вопроса с 1 ответом не было получено ответа");
                    }

                    break;
                }
                case QuestionType.ManyAnswers:
                {
                    if (dQuestion.SelectedAnswerIds.Count > eQuestion.AnswerOptions.Count)
                    {
                        // unreal case
                        throw new BusinessException("Для вопроса было получено больше ответов чем есть");
                    }
                    else if (dQuestion.SelectedAnswerIds.Count == 0)
                    {
                        throw new BusinessException("Для вопроса не было получено ответов");
                    }

                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(eQuestion.QuestionType));
            }


            // проверяем ответы в БД
            foreach (var option in eQuestion.AnswerOptions)
            {
                // если ответ в БД - правильный и содержится в полученных
                if (option.IsCorrect && dQuestion.SelectedAnswerIds.Contains(option.Id))
                {
                    // засчитываем его
                    totalWeight += option.Weight;
                }
            }
        }

        var maxWeight = exam.Questions
            .Select(
                q => q.AnswerOptions
                    .Where(a => a.IsCorrect)
                    .Select(a => a.Weight)
                    .Sum()
            )
            .Sum();

        if (totalWeight == maxWeight)
        {
            if (totalWeight >= exam.MinimumPassingGrade)
            {
                await _courseProgressService.TransitionToPassed(dto.CourseId, userId);
            }
        }

        return true;
    }
}