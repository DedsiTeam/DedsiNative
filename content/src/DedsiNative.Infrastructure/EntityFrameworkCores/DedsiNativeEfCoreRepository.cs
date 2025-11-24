using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Dedsi.Repositories;

namespace DedsiNative.EntityFrameworkCores;

public class DedsiNativeEfCoreRepository<TDomain, TPrimaryKey>(DedsiNativeDbContext dedsiNativeDbContext)
    : IDedsiNativeRepository<TDomain, TPrimaryKey>
    where TDomain : class
{
    protected virtual DbSet<TDomain> DbSet => dedsiNativeDbContext.Set<TDomain>();

    /// <summary>
    /// 获取主键表达式用于查询
    /// </summary>
    /// <param name="id">主键值</param>
    /// <returns>主键表达式</returns>
    protected virtual Expression<Func<TDomain, bool>> GetPrimaryKeyExpression(TPrimaryKey id)
    {
        // 使用 EF.Property 方法来动态获取主键属性
        // 这是一个通用的解决方案，适用于大多数实体
        var parameter = Expression.Parameter(typeof(TDomain), "e");
        var primaryKeyProperty = dedsiNativeDbContext.Model.FindEntityType(typeof(TDomain))?.FindPrimaryKey()?.Properties.FirstOrDefault();

        if (primaryKeyProperty == null)
        {
            throw new InvalidOperationException($"{typeof(TDomain).Name} 找不到主键");
        }

        var propertyAccess = Expression.Call(
            typeof(EF),
            nameof(EF.Property),
            [typeof(TPrimaryKey)],
            parameter,
            Expression.Constant(primaryKeyProperty.Name));

        var equal = Expression.Equal(propertyAccess, Expression.Constant(id));

        return Expression.Lambda<Func<TDomain, bool>>(equal, parameter);
    }

    /// <inheritdoc/>
    public virtual async Task<bool> DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken)
    {
        var entity = await GetAsync(id, cancellationToken);

        DbSet.Remove(entity);
        var result = await dedsiNativeDbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    /// <inheritdoc/>
    public virtual async Task<TDomain> GetAsync(TPrimaryKey id, CancellationToken cancellationToken)
    {
        // 根据主键查找实体
        var entity = await FindAsync(GetPrimaryKeyExpression(id), cancellationToken);

        return entity ?? throw new KeyNotFoundException($"不存在 id {id} 的实体。");
    }

    /// <inheritdoc/>
    public virtual async Task<bool> InsertAsync(TDomain entity, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        var result = await dedsiNativeDbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    /// <inheritdoc/>
    public virtual async Task<bool> UpdateAsync(TDomain entity, CancellationToken cancellationToken)
    {
        DbSet.Update(entity);
        var result = await dedsiNativeDbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    public virtual Task<TDomain?> FindAsync(Expression<Func<TDomain, bool>> wherePredicate, CancellationToken cancellationToken)
    {
        return DbSet.FirstOrDefaultAsync(wherePredicate, cancellationToken);
    }
}