namespace WebModel.Responses.CategoryResponses;

public class GetCategoryResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid? CategoryFatherId { get; set; }
    public IEnumerable<GetCategoryResponse>? SubCategories { get; set; } = null;


    public override bool Equals(object? toCompare)
    {
        GetCategoryResponse? objectToCompareWith = toCompare as GetCategoryResponse;
        return Id == objectToCompareWith.Id && Name == objectToCompareWith.Name;
    }
}