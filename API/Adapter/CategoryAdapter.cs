using Adapter.CustomExceptions;
using Domain;
using IAdapter;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.CategoryRequests;
using WebModel.Responses.CategoryResponses;

namespace Adapter;

public class CategoryAdapter : ICategoryAdapter
{
    #region Constructor and atributtes

    private readonly ICategoryService _categoryServiceLogic;

    public CategoryAdapter(ICategoryService categoryServiceLogic)
    {
        _categoryServiceLogic = categoryServiceLogic;
    }

    #endregion

    #region Get All Categories

    public IEnumerable<GetCategoryResponse> GetAllCategories()
    {
        try
        {
            IEnumerable<CategoryComponent> categories = _categoryServiceLogic.GetAllCategories();
            IEnumerable<GetCategoryResponse> categoriesResponses = categories.Select(category => new GetCategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            });

            return categoriesResponses;
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    #endregion

    #region Get Category By Id
    public GetCategoryResponse GetCategoryById(Guid idOfCategoryToFind)
    {
        try
        {
            CategoryComponent category = _categoryServiceLogic.GetCategoryById(idOfCategoryToFind);
            GetCategoryResponse categoryResponse = new GetCategoryResponse { Id = category.Id, Name = category.Name };
            return categoryResponse;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException();
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }
    
    #endregion
    
    #region Create Category

    public CreateCategoryResponse CreateCategory(CreateCategoryRequest categoryToCreate)
    {
        try
        {
            CategoryComponent category = new Category
            {
                Id = Guid.NewGuid(),
                Name = categoryToCreate.Name,
                CategoryFatherId = categoryToCreate.CategoryFatherId
            };

            _categoryServiceLogic.CreateCategory(category);

            CreateCategoryResponse categoryResponse = new CreateCategoryResponse { Id = category.Id };

            return categoryResponse;
        }
        catch (ObjectRepeatedServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (ObjectErrorServiceException exceptionCaught)
        {
            throw new ObjectErrorAdapterException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }
    
    #endregion
}