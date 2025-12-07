using System.ComponentModel.DataAnnotations;

namespace WebApi.Application.DTOs.User;
public record CreateUserDTO(
    [Required(ErrorMessage = "Nome é obrigatório")]
    string Name,
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    string Email
);

