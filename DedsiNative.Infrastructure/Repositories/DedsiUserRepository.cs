using DedsiNative.DedsiUsers;

namespace DedsiNative.Repositories;

public class DedsiUserRepository: IDedsiUserRepository
{
    public Task<bool> InsertAsync(DedsiUser dedsiUser, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(DedsiUser dedsiUser, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<DedsiUser?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<DedsiUser?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}