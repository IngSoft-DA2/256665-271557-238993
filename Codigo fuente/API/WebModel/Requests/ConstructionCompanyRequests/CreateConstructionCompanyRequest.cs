namespace WebModel.Requests.ConstructionCompanyRequests;

public class CreateConstructionCompanyRequest
{
    public string Name { get; set; }
    public Guid UserCreatorId { get; set; }
}