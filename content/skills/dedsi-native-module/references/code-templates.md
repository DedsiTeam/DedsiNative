# 代码骨架模板

将 `DedsiUser` 替换为你的模块名（例如 `Order`、`Product`），并按目录落位。
占位符说明：
- `<Module>`：实体名（单数，如 `Order`）
- `<ModulePlural>`：Domain/Operation 分组（如 `Orders`）
- `<ApiGroup>`：HttpApi 分组目录（可与 `<ModulePlural>` 相同，也可按现有风格简化，如 `Users`）
- `<module-plural-kebab>`：路由复数短横线（如 `orders`）

## Domain Entity

```csharp
namespace DedsiNative.<ModulePlural>;

public class <Module>
{
    protected <Module>() {}

    public <Module>(string id, string name)
    {
        Id = id;
        ChangeName(name);
    }

    public string Id { get; private set; } = null!;
    public string Name { get; private set; } = null!;

    public <Module> ChangeName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("名称不能为空。", nameof(name));

        Name = name.Trim();
        return this;
    }
}
```

## Domain Repository Interface

```csharp
using Dedsi.Repositories;

namespace DedsiNative.<ModulePlural>;

public interface I<Module>Repository : IDedsiNativeRepository<<Module>, string>;
```

## Infrastructure DbContext

```csharp
using DedsiNative.<ModulePlural>;
using Microsoft.EntityFrameworkCore;

namespace DedsiNative.EntityFrameworkCores;

public class DedsiNativeDbContext(DbContextOptions<DedsiNativeDbContext> options) : DbContext(options)
{
    public DbSet<<Module>> <ModulePlural> { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (modelBuilder is null)
        {
            throw new ArgumentNullException(nameof(modelBuilder));
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DedsiNativeDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
```

## Infrastructure EntityConfiguration

```csharp
using DedsiNative.<ModulePlural>;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DedsiNative.EntityFrameworkCores.EntityConfigurations;

internal class <Module>Configuration : IEntityTypeConfiguration<<Module>>
{
    public void Configure(EntityTypeBuilder<<Module>> builder)
    {
        builder.ToTable("<TableName>");
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasMaxLength(26)
            .IsRequired();

        builder.Property(x => x.Name)
            .HasMaxLength(64)
            .IsRequired();
    }
}
```

## Operation (Create)

```csharp
using Dedsi;

namespace DedsiNative.<ModulePlural>.Operations;

public record Create<Module>InputDto(string Name);

public class Create<Module>Operation(I<Module>Repository repository)
    : DedsiNativeOperation<Create<Module>InputDto, bool>
{
    public override Task<bool> ExecuteAsync(Create<Module>InputDto input, CancellationToken cancellationToken)
    {
        var entity = new <Module>(GetStringPrimaryKey(), input.Name);
        return repository.InsertAsync(entity, cancellationToken);
    }
}
```

## Infrastructure Repository

```csharp
using DedsiNative.<ModulePlural>;
using DedsiNative.EntityFrameworkCores;

namespace DedsiNative.Repositories;

public class <Module>Repository(DedsiNativeDbContext dbContext)
    : DedsiNativeEfCoreRepository<<Module>, string>(dbContext), I<Module>Repository;
```

## HttpApi Endpoint

```csharp
using DedsiNative.<ModulePlural>.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.Apis.<ApiGroup>;

public static class <Module>Endpoints
{
    public static void Map<Module>Endpoints(this IEndpointRouteBuilder endpoints)
    {
        var group = endpoints.MapGroup("/api/<module-plural-kebab>").WithTags("<Module>");

        group.MapPost("/", (
            [FromBody] Create<Module>InputDto input,
            [FromServices] Create<Module>Operation operation,
            CancellationToken cancellationToken) => operation.ExecuteAsync(input, cancellationToken));
    }
}
```

## HttpApi 聚合入口挂载

```csharp
using DedsiNative.Apis.<ApiGroup>;

namespace DedsiNative.Apis;

public static class DedsiNativeEndpoints
{
    public static void MapDedsiNativeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.Map<Module>Endpoints();
    }
}
```
