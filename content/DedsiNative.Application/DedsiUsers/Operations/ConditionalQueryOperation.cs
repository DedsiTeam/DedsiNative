using DedsiNative.DedsiUsers.Dtos;
using DedsiNative.EntityFrameworkCores;
using Microsoft.EntityFrameworkCore;

namespace DedsiNative.DedsiUsers.Operations;

public class ConditionalQueryOperation(DedsiNativeDbContext dedsiNativeDbContext): DedsiNativeQueryOperation<DedsiUserPagedQueryInputDto, DedsiUserPagedQueryResultDto>
{
    public override async Task<DedsiUserPagedQueryResultDto> ExecuteAsync(DedsiUserPagedQueryInputDto input, CancellationToken cancellationToken)
    {
        var query = dedsiNativeDbContext.DedsiUsers.AsQueryable()
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