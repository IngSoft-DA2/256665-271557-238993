namespace WebModel.Requests.ConstructionCompanyRequests;

public class CreateConstructionCompanyRequest
{
    public string Name { get; set; }
    
    public Guid UserCreator { get; set; }
}