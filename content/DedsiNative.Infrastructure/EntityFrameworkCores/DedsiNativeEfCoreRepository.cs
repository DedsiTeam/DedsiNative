using DedsiNative.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DedsiNative.EntityFrameworkCores;

public class DedsiNativeEfCoreRepository<TEntity, TPrimaryKey>(DedsiNativeDbContext dedsiNativeDbContext)
    : IDedsiNativeRepository<TEntity, TPrimaryKey>
    where TEntity : class
{
    protected virtual DbSet<TEntity> DbSet => dedsiNativeDbContext.Set<TEntity>();

    /// <summary>
    /// 获取主键表达式用于查询
    /// </summary>
    /// <param name="id">主键值</param>
    /// <returns>主键表达式</returns>
    protected virtual Expression<Func<TEntity, bool>> GetPrimaryKeyExpression(TPrimaryKey id)
    {
        // 使用 EF.Property 方法来动态获取主键属性
        // 这是一个通用的解决方案，适用于大多数实体
        var parameter = Expression.Parameter(typeof(TEntity), "e");
        var primaryKeyProperty = dedsiNativeDbContext.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey()?.Properties.FirstOrDefault();

        if (primaryKeyProperty == null)
        {
            throw new InvalidOperationException($"{typeof(TEntity).Name} 找不到主键");
        }

        var propertyAccess = Expression.Call(
            typeof(EF),
            nameof(EF.Property),
            [typeof(TPrimaryKey)],
            parameter,
            Expression.Constant(primaryKeyProperty.Name));

        var equal = Expression.Equal(propertyAccess, Expression.Constant(id));

        return Expression.Lambda<Func<TEntity, bool>>(equal, parameter);
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
    public virtual async Task<TEntity> GetAsync(TPrimaryKey id, CancellationToken cancellationToken)
    {
        // 根据主键查找实体
        var entity = await DbSet.FirstOrDefaultAsync(GetPrimaryKeyExpression(id), cancellationToken);

        if (entity == null)
        {
            throw new KeyNotFoundException($"Entity with id {id} not found.");
        }

        return entity;
    }

    /// <inheritdoc/>
    public virtual async Task<bool> InsertAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await DbSet.AddAsync(entity, cancellationToken);
        var result = await dedsiNativeDbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }

    /// <inheritdoc/>
    public virtual async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
    {
        DbSet.Update(entity);
        var result = await dedsiNativeDbContext.SaveChangesAsync(cancellationToken);
        return result > 0;
    }
}