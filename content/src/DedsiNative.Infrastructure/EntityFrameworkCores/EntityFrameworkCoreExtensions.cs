using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace DedsiNative.EntityFrameworkCores;

public static class EntityFrameworkCoreExtensions
{
    /// <summary>
    /// 使用 MySQL 配置并注册 DedsiNativeDbContext。
    /// 从配置项 ConnectionStrings:Default 读取连接字符串，并启用失败重试策略。
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">应用配置（用于读取连接字符串）</param>
    /// <returns>IServiceCollection，便于链式调用</returns>
    /// <exception cref="InvalidOperationException">当未配置 ConnectionStrings:DedsiNativeDB 时抛出</exception>
    public static IServiceCollection AddMySqlDb(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DedsiNativeDB");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException("ConnectionStrings:DedsiNativeDB is not configured.");
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
    /// 按条件追加 Where 子句：当 <paramref name="condition"/> 为 true 时应用 <paramref name="predicate"/>，否则返回原查询。
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">查询源</param>
    /// <param name="condition">条件布尔值</param>
    /// <param name="predicate">过滤表达式</param>
    /// <returns>可能被过滤后的查询</returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition ? query.Where(predicate) : query;
    }

    /// <summary>
    /// 当字符串 <paramref name="value"/> 非空且不全是空白时，追加 Where 子句；否则返回原查询。
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">查询源</param>
    /// <param name="value">用于判定的字符串值</param>
    /// <param name="predicate">过滤表达式</param>
    /// <returns>可能被过滤后的查询</returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, string? value, Expression<Func<T, bool>> predicate)
    {
        return !string.IsNullOrWhiteSpace(value) ? query.Where(predicate) : query;
    }

    /// <summary>
    /// 当可空值类型 <paramref name="value"/> 具有值（HasValue 为 true）时，追加 Where 子句；否则返回原查询。
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    /// <param name="query">查询源</param>
    /// <param name="value">可空值类型的值</param>
    /// <param name="predicate">过滤表达式</param>
    /// <returns>可能被过滤后的查询</returns>
    public static IQueryable<T> WhereIf<T, TValue>(this IQueryable<T> query, TValue? value, Expression<Func<T, bool>> predicate)
        where TValue : struct
    {
        return value.HasValue ? query.Where(predicate) : query;
    }

    /// <summary>
    /// 对已排序的查询进行分页：当 <paramref name="isPaged"/> 为 true 时，按 <paramref name="pageIndex"/>（从 1 开始）和 <paramref name="pageSize"/> 执行 Skip/Take；否则返回原查询。
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="query">已排序的查询源（确保确定性分页）</param>
    /// <param name="isPaged">是否进行分页</param>
    /// <param name="pageIndex">页码（从 1 开始）</param>
    /// <param name="pageSize">每页大小</param>
    /// <returns>分页后的查询或原查询</returns>
    public static IQueryable<T> PagedBy<T>(this IOrderedQueryable<T> query, bool? isPaged, int pageIndex, int pageSize)
    {
        return isPaged == true ? query.Skip((pageIndex - 1) * pageSize).Take(pageSize) : query;
    }
}
