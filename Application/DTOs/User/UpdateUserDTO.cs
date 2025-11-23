using System.ComponentModel.DataAnnotations;

namespace API.Application.DTOs;
public record UpdateUserDTO(
    string? Name,
    [EmailAddress(ErrorMessage = "Email inválido")]
    string? Email
);
