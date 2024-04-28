using Domain;

namespace IServiceLogic;

public interface ICategoryService
{
    public IEnumerable<Category> GetAllCategories();
    public Category GetCategoryById(Guid idOfCategoryToFind);
    public void CreateCategory(Category categoryToCreate);
}