namespace WebApi.Application.DTOs.User;
public record UserDTO(
    Guid Id,
    string Name,
    string Email,
    DateTime CreatedAt,
    DateTime UpdatedAt
);