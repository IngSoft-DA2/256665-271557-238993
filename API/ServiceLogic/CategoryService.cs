using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class CategoryService : ICategoryService
{
    #region Constructor and dependency injection

    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    #endregion

    #region Get all categories

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

    #endregion

    #region Get Category By Id

    public Category GetCategoryById(Guid categoryToGetId)
    {
        Category category;
        try
        {
            category = _categoryRepository.GetCategoryById(categoryToGetId);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }

        if (category is null) throw new ObjectNotFoundServiceException();

        return category;
    }

    #endregion

    public void CreateCategory(Category categoryToCreate)
    {
        try
        {
            categoryToCreate.CategoryValidator();
            _categoryRepository.CreateCategory(categoryToCreate);
        }
        catch (InvalidCategoryException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
      
    }
}