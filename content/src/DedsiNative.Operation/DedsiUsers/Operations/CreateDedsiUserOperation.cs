using Dedsi;

namespace DedsiNative.DedsiUsers.Operations;

public record CreateDedsiUserInputDto
(
    string Name,
    string Email,
    string MobilePhone
);

/// <summary>
/// 创建用户操作
/// </summary>
/// <param name="dedsiUserRepository"></param>
public class CreateDedsiUserOperation(IDedsiUserRepository dedsiUserRepository) : DedsiNativeOperation<CreateDedsiUserInputDto, bool>
{
    /// <inheritdoc/>
    public override Task<bool> ExecuteAsync(CreateDedsiUserInputDto input, CancellationToken cancellationToken)
    {
        var dedsiUser = new DedsiUser(
            GetStringPrimaryKey(),
            input.Name,
            input.Email,
            input.MobilePhone
        );

        return dedsiUserRepository.InsertAsync(dedsiUser, cancellationToken);
    }
}
