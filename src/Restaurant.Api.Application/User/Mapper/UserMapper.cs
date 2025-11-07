using Restaurant.Api.Application.User.Commands.Registry;
using Restaurant.Api.Application.User.Commands.Update;
using Restaurant.Api.Application.User.Dtos;
using Restaurant.Api.Core.Entities;
using CoreUser = Restaurant.Api.Core.Entities.User;

namespace Restaurant.Api.Application.User.Mapper;

public class UserMapper {
    public static UserDto ToDto(CoreUser User) {
        if (User == null)
            throw new ArgumentNullException(nameof(User));
        return new UserDto {
            Id = User.Id.ToString(),
            Name = User.Name,
            Username = User.Username,
            Password = "************",
            RoleId = User.RoleId.ToString(),
            CreatedAt = User.CreatedAt,
            UpdatedAt = User.UpdatedAt
        };
    }
    public static CoreUser Registry(RegistryCommand command)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));
        return new CoreUser
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Username = command.Username,
            Password = command.Password,
            RoleId = Guid.Parse(command.RoleId),
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now
        };
    }
    public static CoreUser ToUpdate(UpdateCommand command, CoreUser user)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));
        return new CoreUser
        {
            Id = user.Id,
            Name = command.Name ?? user.Name,
            Username = command.Username ?? user.Username,
            Password = user.Password,
            RoleId = user.RoleId,
            CreatedAt = user.CreatedAt,
            UpdatedAt = DateTime.Now
        };
    }

    public static CoreUser ToChangePassword(UpdateCommand command, CoreUser user)
    {
        if (command == null)
            throw new ArgumentNullException(nameof(command));
        return new CoreUser
        {
            Id = user.Id,
            Name = command.Name ?? user.Name,
            Username = command.Username ?? user.Username,
            Password = user.Password,
            RoleId = user.RoleId,
            CreatedAt = user.CreatedAt,
            UpdatedAt = DateTime.Now
        };
    }
    
    public static CoreUser ToEntity(UserDto user) {
        return new CoreUser {
            Id = Guid.Parse(user.Id),
            Name = user.Name,
            Username = user.Username,
            Password = user.Password,
            RoleId = Guid.Parse(user.RoleId),
            CreatedAt = user.CreatedAt,
            UpdatedAt = DateTime.Now
        };
    }
}