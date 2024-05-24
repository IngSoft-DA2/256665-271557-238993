namespace Domain;

public class Session
{
    public Guid SessionString { get; set; }
    public Guid UserId { get; set; }
    public string UserRole { get; set; }
    
    public Session()
    {
        SessionString = Guid.NewGuid();
    }
}