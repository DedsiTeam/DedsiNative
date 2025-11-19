using Dedsi.Operation;
using DedsiNative.EntityFrameworkCores;
using Microsoft.EntityFrameworkCore;

namespace DedsiNative.DedsiUsers.Operations;

public class DedsiUserPagedQueryInputDto
{
    public int PageIndex { get; set; } = 1;

    public int PageSize { get; set; } = 10;

    public bool? IsPaged { get; set; } = true;

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

public class ConditionalQueryOperation(DedsiNativeDbContext dedsiNativeDbContext): DedsiNativeOperation<DedsiUserPagedQueryInputDto, DedsiUserPagedQueryResultDto>
{
    public override async Task<DedsiUserPagedQueryResultDto> ExecuteAsync(DedsiUserPagedQueryInputDto input, CancellationToken cancellationToken)
    {
        var query = dedsiNativeDbContext
            .DedsiUsers
            .AsQueryable()
            .WhereIf(input.Name, u => u.Name.Contains(input.Name!))
            .WhereIf(input.Email, u => u.Email.Contains(input.Email!))
            .WhereIf(input.MobilePhone, u => u.MobilePhone.Contains(input.MobilePhone!));

        var totalCount = await query.CountAsync(cancellationToken: cancellationToken);

        var items = await query
            .OrderByDescending(u => u.Name)
            .PagedBy(input.IsPaged, input.PageIndex, input.PageSize)
            .Select(u => new DedsiUserPagedQueryRowDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                MobilePhone = u.MobilePhone
            })
            .ToArrayAsync(cancellationToken);

        return new DedsiUserPagedQueryResultDto()
        {
            TotalCount = totalCount,
            Items = items
        };
    }
}