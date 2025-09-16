namespace DedsiNative.HttpApi.Models;

/// <summary>
/// 统一错误响应模型
/// </summary>
public class ErrorResponse
{
    /// <summary>
    /// 错误代码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 错误消息
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 详细错误信息（仅在开发环境显示）
    /// </summary>
    public string? Details { get; set; }

    /// <summary>
    /// 错误发生时间
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// 请求路径
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// 跟踪ID
    /// </summary>
    public string? TraceId { get; set; }
}
