using Domain;
using IRepository;
using IServiceLogic;
using Moq;
using Repositories.CustomExceptions;
using ServiceLogic;
using ServiceLogic.CustomExceptions;

namespace Test.Services;

[TestClass]
public class CategoryServiceTest
{
    //Happy path
    [TestMethod]
    public void GetAllCategories_CategoriesAreReturned()
    {
        IEnumerable<Category> categoriesInDb = new List<Category>
        {
            new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category1"
            },
            new Category()
            {
                Id = Guid.NewGuid(),
                Name = "Category2"
            }
        };

        Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);

        categoryRepository.Setup(categoryRepository => categoryRepository.GetAllCategories()).Returns(categoriesInDb);

        CategoryService service = new CategoryService(categoryRepository.Object);

        IEnumerable<Category> categories = service.GetAllCategories();

        Assert.AreEqual(categoriesInDb.Count(), categories.Count());
        Assert.IsTrue(categories.SequenceEqual(categoriesInDb));
    }

    [TestMethod]
    public void GetAllCategories_UnknownServiceExceptionIsThrown()
    {
        Mock<ICategoryRepository> categoryRepository = new Mock<ICategoryRepository>(MockBehavior.Strict);

        categoryRepository.Setup(categoryRepository => categoryRepository.GetAllCategories())
            .Throws(new UnknownRepositoryException("Unknown Error"));

        CategoryService service = new CategoryService(categoryRepository.Object);

        Assert.ThrowsException<UnknownServiceException>(() => service.GetAllCategories());
    }
}