using Domain;
using IRepository;
using IServiceLogic;

namespace ServiceLogic;

public class CategoryService
{

    private readonly ICategoryRepository _categoryRepository;
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IEnumerable<Category> GetAllCategories()
    {
        IEnumerable<Category> categories = _categoryRepository.GetAllCategories();
        return categories;
    }
}