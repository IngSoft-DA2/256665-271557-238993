namespace WebModel.Responses.ConstructionCompanyResponses;

public class GetConstructionCompanyResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public override bool Equals(object? obj)
    {
        GetConstructionCompanyResponse? objectToCompare = obj as GetConstructionCompanyResponse;
        return Id == objectToCompare.Id && Name == objectToCompare.Name;

    }
}