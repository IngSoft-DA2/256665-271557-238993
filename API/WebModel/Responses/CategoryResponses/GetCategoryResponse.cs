namespace WebModel.Responses.CategoryResponses;

public class GetCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }


    public override bool Equals(object? toCompare)
    {
        GetCategoryResponse? objectToCompareWith = toCompare as GetCategoryResponse;

        if (objectToCompareWith is null) return false;

        return Id == objectToCompareWith.Id && Name == objectToCompareWith.Name;
    }
}