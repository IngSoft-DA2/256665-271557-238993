﻿using IDataAccess;
using Moq;
using ServiceLogic;


namespace Test.Services;

[TestClass]
public class ConstructionCompanyAdminServiceTest
{
    #region Initialize

    private Mock<IConstructionCompanyAdminRepository> _constructionCompanyAdminRepository;
    private ConstructionCompanyAdminService _constructionCompanyAdminService;

    [TestInitialize]
    public void Initilize()
    {
        _constructionCompanyAdminRepository = new Mock<IConstructionCompanyAdminRepository>(MockBehavior.Strict);
        _constructionCompanyAdminService =
            new ConstructionCompanyAdminService(_constructionCompanyAdminRepository.Object);
    }

    #endregion
}