﻿namespace DedsiAi;

public interface IDedsiAiQueryOperation;

public class DedsiAiOperation
{
    /// <summary>
    /// string 主键生成，使用 ULID 标准
    /// </summary>
    /// <returns></returns>
    protected string GetStringPrimaryKey() => Ulid.NewUlid().ToString();

    /// <summary>
    /// Guid 主键生成，使用 UUIDv7 标准
    /// </summary>
    /// <returns></returns>
    protected Guid GetGuidPrimaryKey() => Guid.CreateVersion7();
}

public interface IDedsiAiQueryOperation<TResult> : IDedsiAiQueryOperation
{
    Task<TResult> ExecuteAsync(CancellationToken cancellationToken);
}

public abstract class DedsiAiOperation<TResult>: DedsiAiOperation, IDedsiAiQueryOperation<TResult>
{
    /// <summary>
    /// 子类必须实现此方法以执行查询操作
    /// </summary>
    /// <param name="input"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<TResult> ExecuteAsync(CancellationToken cancellationToken);
}

public interface IDedsiAiQueryOperation<TInput, TResult> : IDedsiAiQueryOperation
{
    Task<TResult> ExecuteAsync(TInput input, CancellationToken cancellationToken);
}

public abstract class DedsiAiOperation<TInput, TResult> : DedsiAiOperation, IDedsiAiQueryOperation<TInput, TResult>
{
    /// <summary>
    /// 子类必须实现此方法以执行查询操作
    /// </summary>
    /// <param name="input"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public abstract Task<TResult> ExecuteAsync(TInput input, CancellationToken cancellationToken);
}
