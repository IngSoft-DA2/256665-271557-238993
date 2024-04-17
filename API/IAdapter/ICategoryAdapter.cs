using WebModel.Responses.CategoryResponses;

namespace IAdapter;

public interface ICategoryAdapter
{
    public IEnumerable<GetCategoryResponse> GetAllCategories();
}