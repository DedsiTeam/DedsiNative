using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.HttpApi.Apis.DedsiUsers;

/// <summary>
/// ���� Dedsi �û� API
/// </summary>
public static class CreateDedsiUserApi
{
    /// <summary>
    /// ���ô����û��˵�
    /// </summary>
    /// <param name="group">·����</param>
    public static void MapCreateDedsiUser(this RouteGroupBuilder group)
    {
        group.MapPost("/", CreateDedsiUser)
            .WithName("CreateDedsiUser")
            .WithSummary("���� Dedsi �û�")
            .WithDescription("����һ���µ� Dedsi �û�");
    }

    /// <summary>
    /// ���� Dedsi �û�
    /// </summary>
    /// <param name="input">�û�������Ϣ</param>
    /// <param name="operation">�����û���������</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>�������</returns>
    private static Task<bool> CreateDedsiUser(
        [FromBody] CreateDedsiUserInputDto input,
        [FromServices] CreateDedsiUserOperation operation,
        CancellationToken cancellationToken)
    {
        return operation.ExecuteAsync(input, cancellationToken);
    }
}