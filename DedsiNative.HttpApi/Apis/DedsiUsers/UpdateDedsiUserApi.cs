using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.HttpApi.Apis.DedsiUsers;

/// <summary>
/// ���� Dedsi �û� API
/// </summary>
public static class UpdateDedsiUserApi
{
    /// <summary>
    /// ���ø����û�·��
    /// </summary>
    /// <param name="group">·����</param>
    public static void MapUpdateDedsiUser(this RouteGroupBuilder group)
    {
        group.MapPut("/{id}", UpdateDedsiUser)
            .WithName("UpdateDedsiUser")
            .WithSummary("���� Dedsi �û�")
            .WithDescription("����ָ���� Dedsi �û���Ϣ");
    }

    /// <summary>
    /// ���� Dedsi �û�
    /// </summary>
    /// <param name="id">�û�ID</param>
    /// <param name="input">�����û�����Ϣ</param>
    /// <param name="operation">�����û�����ʵ��</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>�������</returns>
    private static Task<bool> UpdateDedsiUser(
        [FromRoute] string id,
        [FromBody] UpdateDedsiUserRequestDto input,
        [FromServices] UpdateDedsiUserOperation operation,
        CancellationToken cancellationToken)
    {
        var operationInput = new UpdateDedsiUserInputDto(id, input.UserName, input.Email, input.MobilePhone);
        return operation.ExecuteAsync(operationInput, cancellationToken);
    }
}

/// <summary>
/// �����û����� DTO������API���棬������ID��
/// </summary>
/// <param name="UserName">�û���</param>
/// <param name="Email">����</param>
/// <param name="MobilePhone">�ֻ���</param>
public record UpdateDedsiUserRequestDto
(
    string UserName,
    string Email,
    string MobilePhone
);