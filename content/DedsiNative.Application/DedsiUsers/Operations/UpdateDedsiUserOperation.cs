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
/// �����û�����
/// </summary>
/// <param name="dedsiUserRepository"></param>
public class UpdateDedsiUserOperation(IDedsiUserRepository dedsiUserRepository) : DedsiNativeOperation<UpdateDedsiUserInputDto, bool>
{
    /// <inheritdoc/>
    public override async Task<bool> ExecuteAsync(UpdateDedsiUserInputDto input, CancellationToken cancellationToken)
    {
        // ��ȡ�����û�
        var dedsiUser = await dedsiUserRepository.GetAsync(input.Id, cancellationToken);
        
        // �����û���Ϣ
        dedsiUser.Update(input.UserName, input.Email, input.MobilePhone);
        
        // ���浽���ݿ�
        return await dedsiUserRepository.UpdateAsync(dedsiUser, cancellationToken);
    }
}