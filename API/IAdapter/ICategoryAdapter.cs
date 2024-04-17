using WebModel.Requests.CategoryRequests;
using WebModel.Responses.CategoryResponses;

namespace IAdapter;

public interface ICategoryAdapter
{
    public IEnumerable<GetCategoryResponse> GetAllCategories();
    public CreateCategoryResponse CreateCategory(CreateCategoryRequest categoryToCreate);
}