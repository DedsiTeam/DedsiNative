using DedsiNative.DedsiUsers.Queries;
using DedsiNative.DedsiUsers.Queries.Dtos;
using DedsiNative.Middleware;
using Microsoft.AspNetCore.Mvc;

namespace DedsiNative.HttpApi.Apis.DedsiUsers;

/// <summary>
/// ��ҳ��ѯ�û� API
/// </summary>
public static class GetDedsiUsersPagedQueryApi
{
    /// <summary>
    /// ���÷�ҳ��ѯ�û��˵�
    /// </summary>
    /// <param name="group">·����</param>
    public static void MapGetDedsiUsersPagedQuery(this RouteGroupBuilder group)
    {
        group.MapPost("/paged", GetDedsiUsersPagedQuery)
            .WithName("GetDedsiUsersPagedQuery")
            .WithSummary("��ҳ��ѯ�û�")
            .WithDescription("����������ҳ��ѯ�û��б�");
    }

    /// <summary>
    /// ��ҳ��ѯ�û�
    /// </summary>
    /// <param name="input">���</param>
    /// <param name="query">�û���ѯ����</param>
    /// <param name="cancellationToken">ȡ������</param>
    /// <returns>��ҳ��ѯ���</returns>
    private static Task<DedsiUserPagedQueryResultDto> GetDedsiUsersPagedQuery(
        [FromBody] DedsiUserPagedQueryInputDto input,
        [FromServices] DedsiUserQuery query,
        CancellationToken cancellationToken)
    {
        return query.ConditionalQueryAsync(input, true, cancellationToken);
    }
}