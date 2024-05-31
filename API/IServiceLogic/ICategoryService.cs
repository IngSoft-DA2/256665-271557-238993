using Domain;

namespace IServiceLogic;

public interface ICategoryService
{
    public IEnumerable<CategoryComponent> GetAllCategories();
    public CategoryComponent GetCategoryById(Guid idOfCategoryToFind);
    public void CreateCategory(CategoryComponent categoryToCreate);
}