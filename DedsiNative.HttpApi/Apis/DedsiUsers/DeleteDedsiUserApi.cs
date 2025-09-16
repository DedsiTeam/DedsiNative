using DedsiNative.DedsiUsers.Operations;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.HttpApi.Apis.DedsiUsers;

/// <summary>
/// ɾ�� Dedsi �û� API
/// </summary>
public static class DeleteDedsiUserApi
{
    /// <summary>
    /// ����ɾ���û�·��
    /// </summary>
    /// <param name="group">·����</param>
    public static void MapDeleteDedsiUser(this RouteGroupBuilder group)
    {
        group.MapDelete("/{id}", DeleteDedsiUser)
            .WithName("DeleteDedsiUser")
            .WithSummary("ɾ�� Dedsi �û�")
            .WithDescription("����IDɾ��ָ���� Dedsi �û�");
    }

    /// <summary>
    /// ɾ�� Dedsi �û�
    /// </summary>
    /// <param name="id">�û�ID</param>
    /// <param name="operation">ɾ���û�����ʵ��</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>�������</returns>
    private static Task<bool> DeleteDedsiUser(
        [FromRoute] string id,
        [FromServices] DeleteDedsiUserOperation operation,
        CancellationToken cancellationToken)
    {
        var input = new DeleteDedsiUserInputDto(id);
        return operation.ExecuteAsync(input, cancellationToken);
    }
}