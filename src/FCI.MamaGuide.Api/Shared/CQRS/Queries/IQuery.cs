using FCI.MamaGuide.Api.Shared.ApiResponse;
using MediatR;

namespace FCI.MamaGuide.Api.Shared.CQRS.Queries;

public interface IQuery : IRequest<Result>
{
}

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}