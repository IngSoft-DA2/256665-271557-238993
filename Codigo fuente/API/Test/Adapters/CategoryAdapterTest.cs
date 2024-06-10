using Adapter;
using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using Moq;
using ServiceLogic.CustomExceptions;
using WebModel.Requests.CategoryRequests;
using WebModel.Responses.CategoryResponses;

namespace Test.Adapters;

[TestClass]
public class CategoryAdapterTest
{
    #region Initializing Aspects

    private Mock<ICategoryService> _categoryServiceLogic;
    private CategoryAdapter _categoryAdapter;
    private CategoryComponent genericCategory1;
    private CategoryComponent genericCategory2;
    private CreateCategoryRequest genericCreateCategoryRequest;

    [TestInitialize]
    public void Initialize()
    {
        _categoryServiceLogic = new Mock<ICategoryService>(MockBehavior.Strict);
        _categoryAdapter = new CategoryAdapter(_categoryServiceLogic.Object);

        genericCategory1 = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Electrician",
            CategoryFatherId = null,
        };

        Guid genericCategory2Id = Guid.NewGuid();
        genericCategory2 = new CategoryComposite()
        {
            Id = genericCategory2Id,
            Name = "Plumber",
            CategoryFatherId = null,
            SubCategories = new List<CategoryComponent>
            {
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Pipe Fitter",
                    CategoryFatherId = genericCategory2Id
                },
                new Category
                {
                    Id = Guid.NewGuid(),
                    Name = "Pipe Welder",
                    CategoryFatherId = genericCategory2Id
                }
            }
        };

        genericCreateCategoryRequest = new CreateCategoryRequest
        {
            Name = "Electrician"
        };
    }

    #endregion

    #region Get All Categories

    [TestMethod]
    public void GetAllCategories_ShouldConvertFromDomainToResponse()
    {
        IEnumerable<CategoryComponent> expectedServiceResponse = new List<CategoryComponent>
            { genericCategory1, genericCategory2 };

        IEnumerable<GetCategoryResponse> expectedAdapterResponse = new List<GetCategoryResponse>
        {
            new GetCategoryResponse
            {
                Id = expectedServiceResponse.First().Id,
                Name = expectedServiceResponse.First().Name,
                SubCategories = null
            },
            new GetCategoryResponse
            {
                Id = expectedServiceResponse.Last().Id,
                Name = expectedServiceResponse.Last().Name,
                SubCategories = new List<GetCategoryResponse>
                {
                    new GetCategoryResponse
                    {
                        Id = expectedServiceResponse.Last().GetChilds().First().Id,
                        Name = expectedServiceResponse.Last().GetChilds().First().Name,
                        SubCategories = null
                    },
                    new GetCategoryResponse
                    {
                        Id = expectedServiceResponse.Last().GetChilds().Last().Id,
                        Name = expectedServiceResponse.Last().GetChilds().Last().Name,
                        SubCategories = null
                    }
                }
            }
        };

        _categoryServiceLogic.Setup(service => service.GetAllCategories()).Returns(expectedServiceResponse);

        IEnumerable<GetCategoryResponse> adapterResponse = _categoryAdapter.GetAllCategories();

        _categoryServiceLogic.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.Count().Equals(expectedServiceResponse.Count()));
        Assert.IsTrue(adapterResponse.SequenceEqual(expectedAdapterResponse));
    }

    [TestMethod]
    public void GetAllCategories_ShouldThrowException()
    {
        _categoryServiceLogic.Setup(service => service.GetAllCategories())
            .Throws(new Exception("Something went wrong"));

        Assert.ThrowsException<Exception>(() => _categoryAdapter.GetAllCategories());
    }

    #endregion

    #region Get Category By Id

    [TestMethod]
    public void GetCategoryById_ShouldConvertFromDomainToResponse()
    {
        CategoryComponent expectedServiceResponse = genericCategory1;

        GetCategoryResponse expectedAdapterResponse = new GetCategoryResponse
        {
            Id = expectedServiceResponse.Id,
            Name = expectedServiceResponse.Name,
            SubCategories = null
        };

        _categoryServiceLogic.Setup(service => service.GetCategoryById(It.IsAny<Guid>()))
            .Returns(expectedServiceResponse);

        GetCategoryResponse adapterResponse = _categoryAdapter.GetCategoryById(It.IsAny<Guid>());

        _categoryServiceLogic.VerifyAll();

        Assert.IsTrue(expectedAdapterResponse.Equals(adapterResponse));
    }

    [TestMethod]
    public void GetCategoryById_ShouldThrowObjectNotFoundException()
    {
        _categoryServiceLogic.Setup(service => service.GetCategoryById(It.IsAny<Guid>()))
            .Throws(new ObjectNotFoundServiceException());

        Assert.ThrowsException<ObjectNotFoundAdapterException>(() =>
            _categoryAdapter.GetCategoryById(It.IsAny<Guid>()));
    }

    [TestMethod]
    public void GetCategoryById_ShouldThrowException()
    {
        _categoryServiceLogic.Setup(service => service.GetCategoryById(It.IsAny<Guid>()))
            .Throws(new Exception("Something went wrong"));

        Assert.ThrowsException<Exception>(() => _categoryAdapter.GetCategoryById(It.IsAny<Guid>()));
    }

    #endregion

    #region Create Category

    [TestMethod]
    public void CreateCategory_ShouldConvertFromRequestToResponse()
    {
        CreateCategoryRequest createCategoryRequest = new CreateCategoryRequest
        {
            Name = "Electrician",
        };

        _categoryServiceLogic.Setup(service => service.CreateCategory(It.IsAny<CategoryComponent>()));

        CreateCategoryResponse adapterResponse = _categoryAdapter.CreateCategory(createCategoryRequest);

        _categoryServiceLogic.Verify(service => service.CreateCategory(It.IsAny<CategoryComponent>()), Times.Once());

        Assert.IsNotNull(adapterResponse);
    }

    [TestMethod]
    public void CreateCategory_ShouldThrowException()
    {
        _categoryServiceLogic.Setup(service => service.CreateCategory(It.IsAny<CategoryComponent>()))
            .Throws(new Exception("Something went wrong"));

        Assert.ThrowsException<Exception>(() => _categoryAdapter.CreateCategory(genericCreateCategoryRequest));

        _categoryServiceLogic.Verify(service => service.CreateCategory(It.IsAny<CategoryComponent>()), Times.Once);
    }

    [TestMethod]
    public void CreateCategory_ShouldThrowObjectRepeatedServiceException()
    {
        _categoryServiceLogic.Setup(service => service.CreateCategory(It.IsAny<CategoryComponent>()))
            .Throws(new ObjectRepeatedServiceException());

        Assert.ThrowsException<ObjectErrorAdapterException>(() =>
            _categoryAdapter.CreateCategory(genericCreateCategoryRequest));

        _categoryServiceLogic.VerifyAll();
    }

    [TestMethod]
    public void CreateCategory_ShouldThrowObjectErrorServiceException()
    {
        _categoryServiceLogic.Setup(service => service.CreateCategory(It.IsAny<CategoryComponent>()))
            .Throws(new ObjectErrorServiceException("Name can't be empty"));

        Assert.ThrowsException<ObjectErrorAdapterException>(() =>
            _categoryAdapter.CreateCategory(genericCreateCategoryRequest));

        _categoryServiceLogic.VerifyAll();
    }

    #endregion
}