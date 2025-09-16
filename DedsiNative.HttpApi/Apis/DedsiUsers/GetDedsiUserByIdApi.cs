using DedsiNative.DedsiUsers.Queries;
using DedsiNative.DedsiUsers.Queries.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.HttpApi.Apis.DedsiUsers;

/// <summary>
/// ���� ID ��ȡ�û� API
/// </summary>
public static class GetDedsiUserByIdApi
{
    /// <summary>
    /// ���ø��� ID ��ȡ�û��˵�
    /// </summary>
    /// <param name="group">·����</param>
    public static void MapGetDedsiUserById(this RouteGroupBuilder group)
    {
        group.MapGet("/{id}", GetDedsiUserById)
            .WithName("GetDedsiUserById")
            .WithSummary("���� ID ��ȡ�û�")
            .WithDescription("ͨ���û� ID ��ȡ�����û���Ϣ");
    }

    /// <summary>
    /// ���� ID ��ȡ�û�
    /// </summary>
    /// <param name="id">�û� ID</param>
    /// <param name="query">�û���ѯ����</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>�û���Ϣ</returns>
    private static Task<DedsiUserDto> GetDedsiUserById(
        [FromRoute] string id, 
        [FromServices] DedsiUserQuery query,
        CancellationToken cancellationToken)
    {
        return query.GetByIdAsync(id, cancellationToken);
    }
}