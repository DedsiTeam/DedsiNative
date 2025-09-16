namespace DedsiNative.DedsiUsers.Queries.Dtos;

public class DedsiUserPagedQueryInputDto
{
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    /// <summary>
    /// 姓名
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号：15888888888
    /// </summary>
    public string? MobilePhone { get; set; }

}

public class DedsiUserPagedQueryResultDto
{
    public int TotalCount { get; set; }

    public IEnumerable<DedsiUserPagedQueryRowDto> Items { get; set; } = [];
}


public class DedsiUserPagedQueryRowDto
{
    public string Id { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 手机号：15888888888
    /// </summary>
    public string MobilePhone { get; set; }
}