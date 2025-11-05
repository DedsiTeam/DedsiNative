using System.Linq.Expressions;

namespace DedsiNative.Repositories;

/// <summary>
/// 通用仓储接口，定义针对领域实体的基础数据访问操作（增、改、删、查）。
/// </summary>
/// <typeparam name="TDomain">领域实体类型（通常为 EF Core 实体或聚合根）。</typeparam>
/// <typeparam name="TPrimaryKey">实体主键类型，例如 string、Guid 或 int。</typeparam>
public interface IDedsiNativeRepository<TDomain, in TPrimaryKey>
{
    /// <summary>
    /// 异步插入一条实体记录。
    /// </summary>
    /// <param name="entity">要插入的实体。</param>
    /// <param name="cancellationToken">取消标记。</param>
    /// <returns>当持久化影响行数 &gt; 0 时返回 <c>true</c>，否则返回 <c>false</c>。</returns>
    Task<bool> InsertAsync(TDomain entity, CancellationToken cancellationToken);

    /// <summary>
    /// 异步更新一条实体记录。
    /// </summary>
    /// <param name="entity">要更新的实体。</param>
    /// <param name="cancellationToken">取消标记。</param>
    /// <returns>当持久化影响行数 &gt; 0 时返回 <c>true</c>，否则返回 <c>false</c>。</returns>
    Task<bool> UpdateAsync(TDomain entity, CancellationToken cancellationToken);

    /// <summary>
    /// 根据主键异步删除一条实体记录。
    /// </summary>
    /// <param name="id">要删除实体的主键值。</param>
    /// <param name="cancellationToken">取消标记。</param>
    /// <returns>当持久化影响行数 &gt; 0 时返回 <c>true</c>，否则返回 <c>false</c>。</returns>
    /// <exception cref="KeyNotFoundException">当指定主键对应的实体不存在时抛出。</exception>
    Task<bool> DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken);

    /// <summary>
    /// 根据主键异步获取实体。
    /// </summary>
    /// <param name="id">要获取实体的主键值。</param>
    /// <param name="cancellationToken">取消标记。</param>
    /// <returns>匹配主键的实体实例。</returns>
    /// <exception cref="KeyNotFoundException">当指定主键对应的实体不存在时抛出。</exception>
    Task<TDomain> GetAsync(TPrimaryKey id, CancellationToken cancellationToken);

    /// <summary>
    /// 根据条件表达式异步获取单个实体（首个匹配项）。
    /// </summary>
    /// <param name="wherePredicate">筛选条件表达式。</param>
    /// <param name="cancellationToken">取消标记。</param>
    /// <returns>匹配条件的实体，若未找到则返回 <c>null</c>。</returns>
    Task<TDomain?> FindAsync(Expression<Func<TDomain, bool>> wherePredicate, CancellationToken cancellationToken);
}