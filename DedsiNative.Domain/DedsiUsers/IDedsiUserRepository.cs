namespace DedsiNative.DedsiUsers;

public interface IDedsiUserRepository
{
    Task<bool> InsertAsync(DedsiUser dedsiUser, CancellationToken cancellationToken);
    
    Task<bool> UpdateAsync(DedsiUser dedsiUser, CancellationToken cancellationToken);
    
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken);
    
    Task<DedsiUser?> GetByIdAsync(string id, CancellationToken cancellationToken);
    
    Task<DedsiUser?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    
}