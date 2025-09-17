namespace DedsiNative.DedsiUsers.Operations;

public record DeleteDedsiUserInputDto
(
    string Id
);

/// <summary>
/// É¾³ýÓÃ»§²Ù×÷
/// </summary>
/// <param name="dedsiUserRepository"></param>
public class DeleteDedsiUserOperation(IDedsiUserRepository dedsiUserRepository) : DedsiNativeQueryOperation<DeleteDedsiUserInputDto, bool>
{
    /// <inheritdoc/>
    public override Task<bool> ExecuteAsync(DeleteDedsiUserInputDto input, CancellationToken cancellationToken)
    {
        return dedsiUserRepository.DeleteAsync(input.Id, cancellationToken);
    }
}