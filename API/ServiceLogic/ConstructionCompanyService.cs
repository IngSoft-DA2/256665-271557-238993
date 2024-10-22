using Domain;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class ConstructionCompanyService : IConstructionCompanyService
{
    #region Constructor and dependency injection

    private readonly IConstructionCompanyRepository _constructionCompanyRepository;

    public ConstructionCompanyService(IConstructionCompanyRepository constructionCompanyRepository)
    {
        _constructionCompanyRepository = constructionCompanyRepository;
    }

    #endregion

    #region Get All Construction Companies

    public IEnumerable<ConstructionCompany> GetAllConstructionCompanies()
    {
        try
        {
            IEnumerable<ConstructionCompany> constructionCompanies =
                _constructionCompanyRepository.GetAllConstructionCompanies();
            return constructionCompanies;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Get Construction Company By Id

    public ConstructionCompany GetConstructionCompanyById(Guid idOfConstructionCompany)
    {
        ConstructionCompany constructionCompanyFound;
        try
        {
            constructionCompanyFound =
                _constructionCompanyRepository.GetConstructionCompanyById(idOfConstructionCompany);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }

        if (constructionCompanyFound is null) throw new ObjectNotFoundServiceException();
        return constructionCompanyFound;
    }

    #endregion

    #region Create Construction Company

    public void CreateConstructionCompany(ConstructionCompany constructionCompanyToAdd)
    {
        try
        {
            constructionCompanyToAdd.ConstructionCompanyValidator();
            CheckIfNameIsUsed(constructionCompanyToAdd.Name);
            _constructionCompanyRepository.CreateConstructionCompany(constructionCompanyToAdd);
        }
        catch (InvalidConstructionCompanyException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch(ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedServiceException();
        }
        
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private void CheckIfNameIsUsed(string nameToCheck)
    {
        IEnumerable<ConstructionCompany> constructionCompaniesInDb = GetAllConstructionCompanies();
        
        if (constructionCompaniesInDb.Any(constructionCompany =>
                constructionCompany.Name.Equals(nameToCheck)))
        {
            throw new ObjectRepeatedServiceException();
        }
    }

    #endregion
}