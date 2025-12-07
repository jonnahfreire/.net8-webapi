namespace WebApi.Domain.Entities;

public class User(string name, string email): BaseEntity
{
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;

    public void Update(string? name, string? email)
    {
        Name = name ?? Name;
        Email = email ?? Email;
    }
}
