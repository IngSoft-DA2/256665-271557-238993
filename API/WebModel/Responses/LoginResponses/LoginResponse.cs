namespace WebModel.Responses.LoginResponses;

public class LoginResponse
{
    public Guid SessionString { get; set; }
    public string UserRole { get; set; }
}