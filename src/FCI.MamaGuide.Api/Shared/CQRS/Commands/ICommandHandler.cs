using FCI.MamaGuide.Api.Shared.ApiResponse;
using MediatR;

namespace FCI.MamaGuide.Api.Shared.CQRS.Commands;

public interface ICommandHandler<TCommand>
    : IRequestHandler<TCommand, Result>
    where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
}