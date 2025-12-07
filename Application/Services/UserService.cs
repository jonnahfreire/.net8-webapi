using WebApi.Domain.Entities;
using WebApi.Application.DTOs.User;
using WebApi.Application.Repository;
using WebApi.Domain.Exceptions;
using ArgumentNullException = WebApi.Domain.Exceptions.ArgumentNullException;

namespace WebApi.Application.Services;

public class UserService(IUserRepository userRepository, ILogger<UserService> logger, IUnitOfWork uow)
{
    public async Task<PagingOptions<IEnumerable<UserDTO>>> GetAllUsersWithPagingOptions(int page, int pageSize)
    {
        logger.LogInformation("Fetching all users from the database.");
        var paging = await userRepository.GetAllWithPagingOptionsAsync(page, pageSize);
        return new PagingOptions<IEnumerable<UserDTO>>(
            paging.PageCount,
            paging.ItemsPerPage,
            paging.CurrentPage,
            paging.ItemsCount,
            paging.Items.Select(user => new UserDTO(
                user.Id,
                user.Name,
                user.Email,
                user.CreatedAt,
                user.UpdatedAt
            ))
        );            
    }
    
    public async Task<IEnumerable<UserDTO>> GetAllUsers()
    {
        logger.LogInformation("Fetching all users from the database.");
        var users = await userRepository.GetAllAsync();
        return users.Select(user =>
        {
            return new UserDTO(
                user.Id,
                user.Name,
                user.Email,
                user.CreatedAt,
                user.UpdatedAt
            );
        });
    }

    public async Task<UserDTO?> GetUserById(Guid id)
    {
        var user = await userRepository.GetByIdAsync(id) ?? throw new NotFoundException("User not found");
        return new UserDTO(
            user.Id,
            user.Name,
            user.Email,
            user.CreatedAt,
            user.UpdatedAt
        );
    }

    public async Task AddUser(CreateUserDTO user)
    {
        if (user == null) throw new ArgumentNullException("User data is required");

        var userAlreadyExists = await userRepository.GetByEmailAsync(user.Email);
        if (userAlreadyExists != null) throw new ConflictException("A user with this email already exists.");

        logger.LogInformation("Adding a new user to the database.");
        await userRepository.AddAsync(new User(user.Name, user.Email));
        await uow.CommitAsync();
    }

    public async Task UpdateUser(Guid id, UpdateUserDTO data)
    {
        if (data == null) throw new ArgumentNullException("User data is required");
        var user = await userRepository.GetByIdAsync(id) ?? throw new NotFoundException("User not found");

        user.Update(data.Name, data.Email);
        logger.LogInformation(message: $"Updating user with id: {id}");
        await userRepository.UpdateAsync(user);
        await uow.CommitAsync();
    }

    public async Task RemoveUser(Guid id)
    {
        var user = await userRepository.GetByIdAsync(id) ?? throw new NotFoundException("User not found");
        user.SoftDelete();
        await userRepository.UpdateAsync(user);
        await uow.CommitAsync();
    }
}
