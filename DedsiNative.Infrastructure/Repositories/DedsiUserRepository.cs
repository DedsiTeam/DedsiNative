using DedsiNative.DedsiUsers;
using DedsiNative.EntityFrameworkCores;

namespace DedsiNative.Repositories;

public class DedsiUserRepository(DedsiNativeDbContext dedsiNativeDbContext) 
    : DedsiNativeEfCoreRepository<DedsiUser, string>(dedsiNativeDbContext), IDedsiUserRepository;