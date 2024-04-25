using FCI.MamaGuide.Api.Shared.ApiResponse;
using MediatR;

namespace FCI.MamaGuide.Api.Shared.CQRS.Queries;

public interface IQueryHandler<TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}