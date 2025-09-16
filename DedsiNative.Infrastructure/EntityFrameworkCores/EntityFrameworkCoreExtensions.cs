using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace DedsiNative.EntityFrameworkCores;

public static class EntityFrameworkCoreExtensions
{
    public static IServiceCollection AddMySqlDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("ConnectionStrings:Default is not configured.");
        }
        services.AddDbContext<DedsiNativeDbContext>(options =>
        {
            var serverVersion = ServerVersion.AutoDetect(connectionString);
            options.UseMySql(connectionString, serverVersion, mySqlOptions =>
            {
                mySqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
            });
        });

        return services;
    }

    /// <summary>
    /// 条件查询扩展方法，当条件为真时才应用 Where 子句
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">查询对象</param>
    /// <param name="condition">条件</param>
    /// <param name="predicate">查询谓词</param>
    /// <returns>查询对象</returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition ? query.Where(predicate) : query;
    }

    /// <summary>
    /// 条件查询扩展方法，当条件不为 null 且不为空字符串时才应用 Where 子句
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">查询对象</param>
    /// <param name="value">字符串值</param>
    /// <param name="predicate">查询谓词</param>
    /// <returns>查询对象</returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, string? value, Expression<Func<T, bool>> predicate)
    {
        return !string.IsNullOrWhiteSpace(value) ? query.Where(predicate) : query;
    }

    /// <summary>
    /// 条件查询扩展方法，当值不为 null 时才应用 Where 子句
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="query">查询对象</param>
    /// <param name="value">值</param>
    /// <param name="predicate">查询谓词</param>
    /// <returns>查询对象</returns>
    public static IQueryable<T> WhereIf<T, TValue>(this IQueryable<T> query, TValue? value, Expression<Func<T, bool>> predicate)
        where TValue : struct
    {
        return value.HasValue ? query.Where(predicate) : query;
    }

    public static IQueryable<T> PagedBy<T>(this IOrderedQueryable<T> query, bool isPaged, int pageIndex, int pageSize)
    {
        return isPaged ? query.Skip((pageIndex - 1) * pageSize).Take(pageSize) : query;
    }
}
