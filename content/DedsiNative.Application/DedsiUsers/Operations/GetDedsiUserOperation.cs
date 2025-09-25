using DedsiAi;

namespace DedsiNative.DedsiUsers.Operations;

public class DedsiUserDto
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


/// <summary>
/// 按照主键Id查询
/// </summary>
/// <param name="dedsiUserRepository"></param>
public class GetDedsiUserOperation(IDedsiUserRepository dedsiUserRepository)
    : DedsiNativeOperation<string, DedsiUserDto>
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