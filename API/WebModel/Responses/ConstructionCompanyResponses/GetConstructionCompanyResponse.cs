using System.Collections;

namespace WebModel.Responses.ConstructionCompanyResponses;

public class GetConstructionCompanyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Guid UserCreatorId { get; set; }

    public IEnumerable<Guid> BuildingsId { get; set; }

    public override bool Equals(object? obj)
    {
        GetConstructionCompanyResponse? objectToCompare = obj as GetConstructionCompanyResponse;
        return Id == objectToCompare.Id && Name == objectToCompare.Name && UserCreatorId ==
            objectToCompare.UserCreatorId && BuildingsId.SequenceEqual(objectToCompare.BuildingsId);
    }
}