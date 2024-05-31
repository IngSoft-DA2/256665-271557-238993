using Domain;
using IDataAccess;
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

    public CategoryComponent GetCategoryById(Guid categoryToGetId)
    {
        CategoryComponent category;
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

    #region Create Category

    public void CreateCategory(CategoryComponent categoryToCreate)
    {
        try
        {
            categoryToCreate.CategoryValidator();
            //Implicates that the category to create is a subCategory of another category
            if(categoryToCreate.CategoryFatherId is not null)
            {
                Guid categoryFatherId = categoryToCreate.CategoryFatherId.Value;
                CategoryComponent categoryFather = GetCategoryById(categoryFatherId);

                if (categoryFather is Category)
                {
                    // Delete it because now it will be a category composite
                    _categoryRepository.DeleteCategory(categoryFather);
                    
                    categoryFather = new CategoryComposite()
                    {
                        Id = categoryFather.Id,
                        Name = categoryFather.Name
                    };
                }
                categoryFather.AddChild(categoryToCreate);
                _categoryRepository.UpdateCategory(categoryFather);
            }
            else
            {
                _categoryRepository.CreateCategory(categoryToCreate);
            }
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

    #endregion
}