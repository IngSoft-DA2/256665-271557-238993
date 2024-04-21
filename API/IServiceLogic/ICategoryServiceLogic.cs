using Domain;

namespace IServiceLogic;

public interface ICategoryServiceLogic
{
    public IEnumerable<Category> GetAllCategories();
    public Category GetCategoryById(Guid idOfCategoryToFind);
}