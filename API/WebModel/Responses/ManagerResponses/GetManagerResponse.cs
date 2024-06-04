using WebModel.Responses.BuildingResponses;
using WebModel.Responses.MaintenanceResponses;

namespace WebModel.Responses.ManagerResponses;

public class GetManagerResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public List<Guid> Buildings  { get; set; }
    
    public List<Guid> MaintenanceRequests  { get; set; }

    public override bool Equals(object? obj)
    {
        GetManagerResponse? objectToCompare = obj as GetManagerResponse;
        
        return Id == objectToCompare.Id && Name == objectToCompare.Name && Email == objectToCompare.Email;
    }
}