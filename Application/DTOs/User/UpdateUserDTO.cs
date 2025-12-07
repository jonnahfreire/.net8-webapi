using System.ComponentModel.DataAnnotations;

namespace WebApi.Application.DTOs.User;
public record UpdateUserDTO(
    string? Name,
    [EmailAddress(ErrorMessage = "Email inválido")]
    string? Email
);
