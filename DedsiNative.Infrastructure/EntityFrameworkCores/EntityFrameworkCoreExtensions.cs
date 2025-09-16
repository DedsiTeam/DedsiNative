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
    /// ������ѯ��չ������������Ϊ��ʱ��Ӧ�� Where �Ӿ�
    /// </summary>
    /// <typeparam name="T">ʵ������</typeparam>
    /// <param name="query">��ѯ����</param>
    /// <param name="condition">����</param>
    /// <param name="predicate">��ѯν��</param>
    /// <returns>��ѯ����</returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
    {
        return condition ? query.Where(predicate) : query;
    }

    /// <summary>
    /// ������ѯ��չ��������������Ϊ null �Ҳ�Ϊ���ַ���ʱ��Ӧ�� Where �Ӿ�
    /// </summary>
    /// <typeparam name="T">ʵ������</typeparam>
    /// <param name="query">��ѯ����</param>
    /// <param name="value">�ַ���ֵ</param>
    /// <param name="predicate">��ѯν��</param>
    /// <returns>��ѯ����</returns>
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, string? value, Expression<Func<T, bool>> predicate)
    {
        return !string.IsNullOrWhiteSpace(value) ? query.Where(predicate) : query;
    }

    /// <summary>
    /// ������ѯ��չ��������ֵ��Ϊ null ʱ��Ӧ�� Where �Ӿ�
    /// </summary>
    /// <typeparam name="T">ʵ������</typeparam>
    /// <typeparam name="TValue">ֵ����</typeparam>
    /// <param name="query">��ѯ����</param>
    /// <param name="value">ֵ</param>
    /// <param name="predicate">��ѯν��</param>
    /// <returns>��ѯ����</returns>
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
