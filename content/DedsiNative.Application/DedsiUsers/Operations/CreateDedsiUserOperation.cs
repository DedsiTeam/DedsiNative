using DedsiAi;

namespace DedsiNative.DedsiUsers.Operations;

public record CreateDedsiUserInputDto
(
    string UserName,
    string Email,
    string MobilePhone
);

/// <summary>
/// 创建用户操作
/// </summary>
/// <param name="dedsiUserRepository"></param>
public class CreateDedsiUserOperation(IDedsiUserRepository dedsiUserRepository) : DedsiAiOperation<CreateDedsiUserInputDto, bool>
{
    /// <inheritdoc/>
    public override Task<bool> ExecuteAsync(CreateDedsiUserInputDto input, CancellationToken cancellationToken)
    {
        var dedsiUser = new DedsiUser(
            GetStringPrimaryKey(),
            input.UserName,
            input.Email,
            input.MobilePhone
        );

        return dedsiUserRepository.InsertAsync(dedsiUser, cancellationToken);
    }
}
