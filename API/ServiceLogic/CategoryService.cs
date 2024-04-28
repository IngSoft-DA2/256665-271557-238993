using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

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
        try
        {
            IEnumerable<Category> categories = _categoryRepository.GetAllCategories();
            return categories;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
       
    }

    public Category GetCategoryById(Guid categoryToGetId)
    {
        Category category = _categoryRepository.GetCategoryById(categoryToGetId);
        if (category is null) throw new ObjectNotFoundServiceException();
            
        return category;
    }
}