using Domain;
using IServiceLogic;
using WebModel.Responses.CategoryResponses;

namespace Adapter;

public class CategoryAdapter
{
    private readonly ICategoryServiceLogic _categoryServiceLogic;

    public CategoryAdapter(ICategoryServiceLogic categoryServiceLogic)
    {
        _categoryServiceLogic = categoryServiceLogic;
    }

    public IEnumerable<GetCategoryResponse> GetAllCategories()
    {
        IEnumerable<Category> categories = _categoryServiceLogic.GetAllCategories();
        IEnumerable<GetCategoryResponse> categoriesResponses = categories.Select(category => new GetCategoryResponse
        {
            Id = category.Id,
            Name = category.Name
        });

        return categoriesResponses;
    }
}