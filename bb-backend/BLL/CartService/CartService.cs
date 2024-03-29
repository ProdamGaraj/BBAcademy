﻿using BLL.Services.Interfaces;
using Infrastructure.Models;
using Infrastructure.Models.Enum;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BLL.CartService;

public class CartService : ICartService
{
    private readonly IRepository<Course> _courseRepository;
    private readonly ICourseProgressService _courseProgressService;

    public CartService(ICourseProgressService courseProgressService, IRepository<Course> courseRepository)
    {
        _courseProgressService = courseProgressService;
        _courseRepository = courseRepository;
    }

    public async Task AddCourse(long courseId, long userId)
    {
        await _courseProgressService.TransitionToCart(courseId, userId);
    }

    public async Task RemoveCourse(long courseId, long userId)
    {
        await _courseProgressService.TransitionToUnknown(courseId, userId);
    }
}