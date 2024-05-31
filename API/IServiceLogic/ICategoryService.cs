using Domain;

namespace IServiceLogic;

public interface ICategoryService
{
    public IEnumerable<Category> GetAllCategories();
    public CategoryComponent GetCategoryById(Guid idOfCategoryToFind);
    public void CreateCategory(CategoryComponent categoryToCreate);
}