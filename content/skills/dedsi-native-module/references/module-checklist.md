# 模块落地清单

## 1. Domain

- 新建实体：`content/src/DedsiNative.Domain/<ModulePlural>/<Module>.cs`
- 新建仓储接口：`content/src/DedsiNative.Domain/<ModulePlural>/I<Module>Repository.cs`
- 校验实体行为方法（`ChangeXxx`），不要把业务校验散落到 Operation。
- 仓储接口继承 `IDedsiNativeRepository<<Module>, string>`。

## 2. Infrastructure

- `content/src/DedsiNative.Infrastructure/EntityFrameworkCores/DedsiNativeDbContext.cs` 新增 `DbSet<Module>`。
- 新建实体配置：
  - `content/src/DedsiNative.Infrastructure/EntityFrameworkCores/EntityConfigurations/<Module>Configuration.cs`
  - 至少包含：`ToTable`、`HasKey`，并按需求补充长度/必填/索引
- 新建仓储实现：
  - `content/src/DedsiNative.Infrastructure/Repositories/<Module>Repository.cs`
  - 继承 `DedsiNativeEfCoreRepository<<Module>, string>` 并实现 `I<Module>Repository`

## 3. Operation

- 新建操作目录：`content/src/DedsiNative.Operation/<ModulePlural>/Operations/`
- 至少覆盖：
  - `Create<Module>Operation`
  - `Update<Module>Operation`
  - `Delete<Module>Operation`
  - `Get<Module>Operation`
  - `ConditionalQueryOperation`
- 输入 DTO 使用 `record`；查询结果 DTO 使用 `class`。
- 创建场景使用 `GetStringPrimaryKey()` 生成字符串主键。
- 条件分页查询复用 `WhereIf`、`PagedBy`。

## 4. HttpApi

- 新建 endpoint 文件：
  - `content/src/DedsiNative.HttpApi/Apis/<ApiGroup>/<Module>Endpoints.cs`
- 在 `content/src/DedsiNative.HttpApi/Apis/DedsiNativeEndpoints.cs` 中追加 `Map<Module>Endpoints()`。
- 路由风格保持 `/api/<kebab-plural-name>`。
- API 层仅编排请求参数与 Operation 调用，不写领域校验。

## 5. 注册与运行

- 确认类名后缀：
  - 仓储：`...Repository`
  - 操作：继承 `DedsiNativeOperation<,>`
- Scrutor 会自动扫描并注入，无需手工注册单个类。
- 确认连接字符串键：`ConnectionStrings:DedsiNativeDB`。

## 6. 自检

- 能编译：`dotnet build content/DedsiNative.sln`
- CRUD + 分页查询 endpoint 可调用
- 异常响应符合统一结构（`code/message/path/traceId/timestamp`）
