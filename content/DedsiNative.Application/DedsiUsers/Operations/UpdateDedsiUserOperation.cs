using DedsiAi;

namespace DedsiNative.DedsiUsers.Operations;

public record UpdateDedsiUserInputDto
(
    string Id,
    string UserName,
    string Email,
    string MobilePhone
);

/// <summary>
/// 更新用户操作
/// </summary>
/// <param name="dedsiUserRepository"></param>
public class UpdateDedsiUserOperation(IDedsiUserRepository dedsiUserRepository) : DedsiNativeOperation<UpdateDedsiUserInputDto, bool>
{
    /// <inheritdoc/>
    public override async Task<bool> ExecuteAsync(UpdateDedsiUserInputDto input, CancellationToken cancellationToken)
    {
        // 获取现有用户
        var dedsiUser = await dedsiUserRepository.GetAsync(input.Id, cancellationToken);
        
        // 更新用户信息
        dedsiUser.Update(input.UserName, input.Email, input.MobilePhone);
        
        // 保存到数据库
        return await dedsiUserRepository.UpdateAsync(dedsiUser, cancellationToken);
    }
}