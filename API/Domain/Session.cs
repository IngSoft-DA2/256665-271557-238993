namespace Domain;

public class Session
{
    public Guid Id { get; set; }
    public Guid SessionString { get; set; }
    public SystemUser User { get; set; }
    
    public Session()
    {
        SessionString = Guid.NewGuid();
    }
}