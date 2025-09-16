namespace DedsiNative.Repositories;

public interface IDedsiNativeRepository<TEntity, TPrimaryKey>
{
    /// <summary>
    /// 插入实体到数据库
    /// </summary>
    /// <param name="entity">要插入的实体对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>插入成功返回 true，失败返回 false</returns>
    Task<bool> InsertAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// 更新数据库中的实体
    /// </summary>
    /// <param name="entity">要更新的实体对象</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>更新成功返回 true，失败返回 false</returns>
    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);

    /// <summary>
    /// 根据主键删除实体
    /// </summary>
    /// <param name="id">实体的主键值</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>删除成功返回 true，失败返回 false</returns>
    Task<bool> DeleteAsync(TPrimaryKey id, CancellationToken cancellationToken);

    /// <summary>
    /// 根据主键获取实体
    /// </summary>
    /// <param name="id">实体的主键值</param>
    /// <param name="cancellationToken">取消令牌</param>
    /// <returns>找到的实体对象，如果不存在则抛出 KeyNotFoundException</returns>
    Task<TEntity> GetAsync(TPrimaryKey id, CancellationToken cancellationToken);
}