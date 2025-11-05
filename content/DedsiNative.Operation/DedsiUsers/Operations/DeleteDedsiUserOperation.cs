namespace DedsiNative.DedsiUsers.Operations;

public record DeleteDedsiUserInputDto
(
    string Id
);

/// <summary>
/// 删除用户操作
/// </summary>
/// <param name="dedsiUserRepository"></param>
public class DeleteDedsiUserOperation(IDedsiUserRepository dedsiUserRepository) : DedsiNativeOperation<DeleteDedsiUserInputDto, bool>
{
    /// <inheritdoc/>
    public override Task<bool> ExecuteAsync(DeleteDedsiUserInputDto input, CancellationToken cancellationToken)
    {
        return dedsiUserRepository.DeleteAsync(input.Id, cancellationToken);
    }
}