using DedsiNative.DedsiUsers.Queries.Dtos;
using DedsiNative.EntityFrameworkCores;
using Microsoft.EntityFrameworkCore;

namespace DedsiNative.DedsiUsers.Queries;

public class DedsiUserQuery(
    IDedsiUserRepository dedsiUserRepository,
    DedsiNativeDbContext dedsiNativeDbContext) : DedsiNativeEfCoreQuery(dedsiNativeDbContext), IDedsiNativeQuery
{

    /// <summary>
    /// 单个Id查询
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DedsiUserDto> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var dedsiUser = await dedsiUserRepository.GetAsync(id, cancellationToken);

        return new DedsiUserDto
        {
            Id = dedsiUser.Id,
            Name = dedsiUser.Name,
            Email = dedsiUser.Email,
            MobilePhone = dedsiUser.MobilePhone
        };
    }


    /// <summary>
    /// 条件查询
    /// </summary>
    /// <param name="input"></param>
    /// <param name="isPaged">是否分页：是true/否false </param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<DedsiUserPagedQueryResultDto> ConditionalQueryAsync(DedsiUserPagedQueryInputDto input, bool isPaged, CancellationToken cancellationToken)
    {
        var query = QueryNoTracking<DedsiUser>()
                    .WhereIf(input.Name, u => u.Name.Contains(input.Name!))
                    .WhereIf(input.Email, u => u.Email.Contains(input.Email!))
                    .WhereIf(input.MobilePhone, u => u.MobilePhone.Contains(input.MobilePhone!));

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(u => u.Name)
            .PagedBy(isPaged, input.PageIndex, input.PageSize)
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
