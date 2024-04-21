using Domain;

namespace IServiceLogic;

public interface ICategoryServiceLogic
{
    public IEnumerable<Category> GetAllCategories();
}