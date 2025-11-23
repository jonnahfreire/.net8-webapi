namespace API.Application.DTOs;
public record UserDTO(
    Guid Id,
    string Name,
    string Email,
    DateTime CreatedAt,
    DateTime UpdatedAt
);