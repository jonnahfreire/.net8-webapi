namespace WebApi.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime UpdatedAt { get; protected set; }
    public DateTime? DeletedAt { get; protected set; }

    public void Init()
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow.ToLocalTime();
        UpdatedAt = DateTime.UtcNow.ToLocalTime();
    }

    public void Touch()
    {
        UpdatedAt = DateTime.UtcNow.ToLocalTime();
    }
    
    public void SoftDelete()
    {
        DeletedAt = DateTime.UtcNow.ToLocalTime();
    }
}
