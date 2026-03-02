---
name: dedsi-native-coding
description: 在 DedsiNative（DDD/CQRS + EF Core + Minimal API）项目中新增或重构业务模块。适用于补齐 Domain/Infrastructure/Operation/HttpApi 全链路、按模板生成 CRUD 与分页查询、检查分层依赖与目录落位是否符合现有架构。
---

# Dedsi Native Coding Skill

## 何时使用
- 用户要求在 `DedsiNative` 中新增业务模块或补齐模块全链路。
- 用户要求参照现有 `DedsiUser` 模板生成实体、仓储、Operation、Minimal API。
- 用户要求检查某模块是否符合当前分层和依赖约束。

## 不适用
- 仅做前端页面/组件改造，不涉及后端模块链路。
- 仅做部署、运维、CI 配置，不涉及 `content/src` 业务代码。

## 项目架构基线（以 content 为准）
- `content/src/DedsiNative.Domain`：实体与仓储接口（示例：`DedsiUsers`）
- `content/src/DedsiNative.Infrastructure`：`DedsiNativeDbContext`、`EntityConfigurations`、仓储实现
- `content/src/DedsiNative.Operation`：`Operations/*Operation.cs`（命令/查询）
- `content/src/DedsiNative.HttpApi`：`Apis/*` 的 Minimal API 路由聚合
- `content/src/DedsiNative.HttpApi/Apis/DedsiNativeEndpoints.cs`：统一挂载入口

执行时只读取必要文件，忽略 `bin/`、`obj/`。

## 执行输入
开始改造前先明确：
- 模块名：单数 `<Module>` 与复数 `<ModulePlural>`（如 `DedsiUser` / `DedsiUsers`）
- 路由名：`/api/<kebab-plural-name>`
- 字段与约束：必填、长度、唯一性、排序与查询条件
- 操作范围：是否包含分页查询与条件筛选

## 标准流程（4 步）
先对照现有 `DedsiUser` 全链路，再按以下顺序落地。

1. `DedsiNative.Domain` 添加领域模型
- 新建实体：`content/src/DedsiNative.Domain/<ModulePlural>/<Module>.cs`
- 将核心校验写入实体行为方法（如 `ChangeName/ChangeEmail`），避免分散在 Operation。
- 新建仓储接口：`content/src/DedsiNative.Domain/<ModulePlural>/I<Module>Repository.cs`
- 接口继承：`IDedsiNativeRepository<<Module>, string>`

2. `DedsiNative.Infrastructure` 完善 `DbContext` 和 `EntityConfigurations`
- 在 `content/src/DedsiNative.Infrastructure/EntityFrameworkCores/DedsiNativeDbContext.cs` 增加 `DbSet<Module>`。
- 新建实体配置：`content/src/DedsiNative.Infrastructure/EntityFrameworkCores/EntityConfigurations/<Module>Configuration.cs`
- 在配置中至少声明：`ToTable`、`HasKey`，并按字段补充长度/必填/索引。
- 新建仓储实现：`content/src/DedsiNative.Infrastructure/Repositories/<Module>Repository.cs`
- 仓储继承：`DedsiNativeEfCoreRepository<<Module>, string>` 并实现 `I<Module>Repository`。

3. `DedsiNative.Operation` 添加对应 Operation
- 新建目录：`content/src/DedsiNative.Operation/<ModulePlural>/Operations/`
- 输入 DTO 优先 `record`；查询输出 DTO 可用 `class`。
- 最少补齐：
  - `Create<Module>Operation`
  - `Update<Module>Operation`
  - `Delete<Module>Operation`
  - `Get<Module>Operation`
  - `ConditionalQueryOperation`
- 操作类统一继承 `DedsiNativeOperation<...>` 并实现 `ExecuteAsync`。
- 创建场景使用 `GetStringPrimaryKey()` 生成字符串主键（ULID）。
- 条件查询复用 `WhereIf`、`PagedBy`。

4. `DedsiNative.HttpApi` 添加对应 Minimal API
- 新建 endpoints：`content/src/DedsiNative.HttpApi/Apis/<ApiGroup>/<Module>Endpoints.cs`
- 在 `content/src/DedsiNative.HttpApi/Apis/DedsiNativeEndpoints.cs` 中挂载 `Map<Module>Endpoints()`。
- 路由风格：`/api/<kebab-plural-name>`。
- API 层只编排输入输出与调用 Operation，异常由全局中间件统一处理。

## 产出清单
- `Domain`：实体 + 仓储接口
- `Infrastructure`：`DbSet` + `EntityConfiguration` + 仓储实现
- `Operation`：CRUD + 条件分页查询
- `HttpApi`：模块 endpoints + 聚合入口挂载

## 依赖约束

保持依赖方向不反转：
- `HttpApi` -> `Operation`
- `Operation` -> `Infrastructure` / `Domain` / `frameworks/Dedsi.Operation`
- `Infrastructure` -> `Domain`
- `Domain` -> `frameworks/Dedsi.Domain`

不要让 `Domain` 直接依赖 EF Core、AspNetCore。

## 完成后自检
- 编译：`dotnet build content/DedsiNative.sln`
- 路由：确认模块 endpoints 已在 `MapDedsiNativeEndpoints` 挂载
- 功能：至少验证 Create / Update / Delete / Get / PagedQuery 可调用
- 异常：确认异常响应仍由统一中间件输出

## 参考资料

- 模块落地清单：`references/module-checklist.md`
- 代码骨架模板：`references/code-templates.md`
