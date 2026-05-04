namespace AgendaAle.Domain.Entities;

public class Appointment
{
public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;       
    public string Description { get; private set; } = string.Empty;
    public DateTime Date { get; private set; }
    
    public Guid UserId { get; private set; }
    
    public User? User { get; private set; }

    protected Appointment() { }

    public Appointment(string title, string description, DateTime date, Guid userId)
    {
        Id = Guid.NewGuid();
        Title = title;
        Description = description;
        Date = date;
        UserId = userId;
    }

    public void Update(string title, string description, DateTime date)
    {
        Title = title;
        Description = description;
        Date = date;
    }
}