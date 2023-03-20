using MediatR;

namespace Bill.Application.Features.Users.Commands.CreateUser;

public record CreateUserCommand(CreateUserDto createUserDto) : IRequest<bool>;