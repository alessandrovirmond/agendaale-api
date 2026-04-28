namespace AgendaAle.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    
    public string ExternalAuthId { get; private set; } = string.Empty; 
    public DateTime CreatedAt { get; private set; }

    protected User() { }

    public User(string email, string name, string externalAuthId)
    {
        Id = Guid.NewGuid();
        Email = email;
        Name = name;
        ExternalAuthId = externalAuthId;
        CreatedAt = DateTime.UtcNow;
    }
}