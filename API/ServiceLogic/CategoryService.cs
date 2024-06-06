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

    public IEnumerable<CategoryComponent> GetAllCategories()
    {
        try
        {
            List<CategoryComponent> categoryComponents = _categoryRepository.GetAllCategories().ToList();

            List<CategoryComponent> response = new List<CategoryComponent>();

            foreach (var categoryComponent in categoryComponents)
            {
                if (categoryComponent is Category && categoryComponent.CategoryFatherId != null)
                {
                    continue;
                }

                response.Add(categoryComponent);
            }

            return response;
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
        CategoryComponent categoryComponent;
        try
        {
            categoryComponent = _categoryRepository.GetCategoryById(categoryToGetId);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }

        if (categoryComponent is null) throw new ObjectNotFoundServiceException();

        return categoryComponent;
    }

    #endregion

    #region Create Category

    public void CreateCategory(CategoryComponent categoryToCreate)
    {
        try
        {
            categoryToCreate.CategoryValidator();
            //Implicates that the category to create is a subCategory of another category
            if (categoryToCreate.CategoryFatherId is not null)
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
                        Name = categoryFather.Name,
                        CategoryFatherId = categoryFather.CategoryFatherId
                    };
                    _categoryRepository.CreateCategory(categoryFather);
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