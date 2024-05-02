using Domain;

namespace IRepository;

public interface ICategoryRepository
{
    public IEnumerable<Category> GetAllCategories();
    public Category GetCategoryById(Guid categoryId);
    public void CreateCategory(Category categoryToAdd);
}