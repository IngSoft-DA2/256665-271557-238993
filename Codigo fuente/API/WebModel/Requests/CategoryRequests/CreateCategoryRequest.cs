namespace WebModel.Requests.CategoryRequests;

public class CreateCategoryRequest
{
    public string Name { get; set; }

    public Guid? CategoryFatherId { get; set; } = null;
}