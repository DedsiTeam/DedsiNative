using DedsiNative.DedsiUsers.Dtos;

namespace DedsiNative.DedsiUsers.Operations;

/// <summary>
/// 按照主键Id查询
/// </summary>
/// <param name="dedsiUserRepository"></param>
public class DedsiUserGetByIdOperation(IDedsiUserRepository dedsiUserRepository)
    : DedsiNativeQueryOperation<string, DedsiUserDto>
{
    public override async Task<DedsiUserDto> ExecuteAsync(string id, CancellationToken cancellationToken)
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
}