using System.Linq.Expressions;
using System.Reflection;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public static class SoftDeleteSetupper
{
    private static LambdaExpression IsDeletedRestriction(Type type)
    {
        var propMethod = typeof(EF).GetMethod(nameof(EF.Property),
            BindingFlags.Static |
            BindingFlags.Public)?.MakeGenericMethod(typeof(bool));

        var parameterExpression = Expression.Parameter(type, "it");
        var constantExpression = Expression.Constant(nameof(Entity.Deleted));

        var methodCallExpression = Expression.Call(
            propMethod ??
            throw new InvalidOperationException(), parameterExpression, constantExpression);

        var falseConst = Expression.Constant(false);
        var expressionCondition = Expression.MakeBinary(ExpressionType.Equal, methodCallExpression, falseConst);

        return Expression.Lambda(expressionCondition, parameterExpression);
    }

    /// <summary>
    /// Этот класс творит магию и на жёсткой рефлексии делает так, чтобы поле Deleted автоматически фильтровалось в запросах
    /// Обойти это ограничение можно используя .IgnoreQueryFilters() на запросе
    /// </summary>
    public static void SetupSoftDelete(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            //other automated configurations left out
            if (!typeof(Entity).IsAssignableFrom(entityType.ClrType)) continue;

            entityType.AddIndex(entityType.FindProperty(nameof(Entity.Deleted)));

            modelBuilder
                .Entity(entityType.ClrType)
                .HasQueryFilter(IsDeletedRestriction(entityType.ClrType));
        }
    }
}