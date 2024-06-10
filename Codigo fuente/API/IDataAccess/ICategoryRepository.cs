using Domain;

namespace IDataAccess;

public interface ICategoryRepository
{
    public IEnumerable<CategoryComponent> GetAllCategories();
    public CategoryComponent GetCategoryById(Guid categoryId);
    public void CreateCategory(CategoryComponent categoryToAdd);
    public void DeleteCategory(CategoryComponent category);
    public void UpdateCategory(CategoryComponent category);
}