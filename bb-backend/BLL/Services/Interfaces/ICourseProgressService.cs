﻿namespace BLL.Services.Interfaces;

public interface ICourseProgressService
{
    Task TransitionToCart(long courseId, long userId);
    Task TransitionToBought(long courseId, long userId);
    Task TransitionToPassed(long courseId, long userId, string certName);
    Task TransitionToUnknown(long courseId, long userId);
    Task TransitionToBought(ICollection<long> courseId, long userId);
}