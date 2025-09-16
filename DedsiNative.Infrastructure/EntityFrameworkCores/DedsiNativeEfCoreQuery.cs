using Microsoft.EntityFrameworkCore;

namespace DedsiNative.EntityFrameworkCores;

/// <summary>
/// 只用于查询: EntityFrameworkCore
/// </summary>
/// <param name="dedsiNativeDbContext"></param>
public class DedsiNativeEfCoreQuery(DedsiNativeDbContext dedsiNativeDbContext)
{
    /// <summary>
    /// 获取不跟踪的查询
    /// </summary>
    protected virtual IQueryable<TEntity> QueryNoTracking<TEntity>() where TEntity : class => dedsiNativeDbContext.Set<TEntity>().AsNoTracking();
}
