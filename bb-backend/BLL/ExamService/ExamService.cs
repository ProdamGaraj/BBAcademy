using System.Collections;
using AutoMapper;
using BLL.DocumentService;
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
    private readonly IRepository<Course> _courseRepository;
    private readonly ICourseProgressService _courseProgressService;
    private readonly IDocumentService _documentService;
    private readonly ILogger<ExamService> _logger;

    public ExamService(ILogger<ExamService> logger, ICourseProgressService courseProgressService, IRepository<Course> courseRepository, IDocumentService documentService)
    {
        _logger = logger;
        _courseProgressService = courseProgressService;
        _courseRepository = courseRepository;
        _documentService = documentService;
    }

    public async Task<SaveCourseExamResultsResult> SaveCourseExamResults(long userId, SaveCourseExamResultsDto dto)
    {
        var exam = await _courseRepository.GetAll()
            .Where(c => c.Id == dto.CourseId)
            .Include(c => c.Exam.Questions.OrderBy(q => q.Order))
            .ThenInclude(q => q.AnswerOptions.OrderBy(a => a.Order))
            .Select(c => c.Exam)
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

        if (totalWeight >= exam.MinimumPassingGrade)
        {
            var template = await _courseRepository.GetAll()
                .Where(c => c.Id == dto.CourseId)
                .Select(c => c.CertificateTemplate)
                .FirstOrDefaultAsync();

            if (template?.MediaPath is null)
            {
                throw new BusinessException("Не удалось создать сертификат");
            }

            if (!File.Exists(template.MediaPath))
            {
                throw new BusinessException("Не найден шаблон сертификата на диске");
            }

            var templatePdf = await File.ReadAllBytesAsync(template.MediaPath);

            var certName = await _documentService.Create("cert.pdf", "generated-certs", templatePdf);
            
            await _courseProgressService.TransitionToPassed(dto.CourseId, userId, certName);

            return new SaveCourseExamResultsResult()
            {
                Passed = true,
                CertName = certName
            };
        }
        else
        {
            
            return new SaveCourseExamResultsResult()
            {
                Passed = false,
                CertName = null
            };
        }
    }
}