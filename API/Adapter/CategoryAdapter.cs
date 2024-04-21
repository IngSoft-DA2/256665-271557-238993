using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.CategoryRequests;
using WebModel.Responses.CategoryResponses;

namespace Adapter;

public class CategoryAdapter
{
    #region Constructor and atributtes

    private readonly ICategoryServiceLogic _categoryServiceLogic;

    public CategoryAdapter(ICategoryServiceLogic categoryServiceLogic)
    {
        _categoryServiceLogic = categoryServiceLogic;
    }

    #endregion

    #region Get All Categories

    public IEnumerable<GetCategoryResponse> GetAllCategories()
    {
        try
        {
            IEnumerable<Category> categories = _categoryServiceLogic.GetAllCategories();
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

    public GetCategoryResponse GetCategoryById(Guid idOfCategoryToFind)
    {
        try
        {
            Category category = _categoryServiceLogic.GetCategoryById(idOfCategoryToFind);
            GetCategoryResponse categoryResponse = new GetCategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };

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

    public CreateCategoryResponse CreateCategory(CreateCategoryRequest categoryToCreate)
    {
        try
        {
            Category category = new Category
            {
                Name = categoryToCreate.Name
            };

            category = _categoryServiceLogic.CreateCategory(category);

            CreateCategoryResponse categoryResponse = new CreateCategoryResponse
            {
                Id = category.Id,
            };

            return categoryResponse;
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
}