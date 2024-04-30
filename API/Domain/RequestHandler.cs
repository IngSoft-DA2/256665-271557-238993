namespace Domain;

public class RequestHandler
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public object FirstName { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public void RequestValidator()
    {
        throw new NotImplementedException();
    }
}