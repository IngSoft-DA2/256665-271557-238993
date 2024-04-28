using Domain;

namespace IRepository;

public interface ICategoryRepository
{
    public IEnumerable<Category> GetAllCategories();
}