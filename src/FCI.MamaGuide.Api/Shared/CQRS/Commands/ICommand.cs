using FCI.MamaGuide.Api.Shared.ApiResponse;
using MediatR;

namespace FCI.MamaGuide.Api.Shared.CQRS.Commands;

public interface ICommand : IRequest<Result>
{
}

public interface ICommand<TResponse> : IRequest<Result<TResponse>>
{
}