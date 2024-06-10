using System.Reflection;
using Domain;
using IDataAccess;
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
            CheckIfNameIsUsedAndUserCreatedACompanyBefore(constructionCompanyToAdd);

            _constructionCompanyRepository.CreateConstructionCompany(constructionCompanyToAdd);
        }
        catch (InvalidConstructionCompanyException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException)
        {
            throw;
        }
        catch (ObjectErrorServiceException)
        {
            throw;
        }

        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private void CheckIfNameIsUsedAndUserCreatedACompanyBefore(ConstructionCompany constructionCompanyToCreate)
    {
        IEnumerable<ConstructionCompany> constructionCompaniesInDb = GetAllConstructionCompanies();

        bool nameExists =
            constructionCompaniesInDb.Any(company => company.Name.Equals(constructionCompanyToCreate.Name));
        if (nameExists)
        {
            throw new ObjectRepeatedServiceException();
        }

        bool userCreatedCompanyBefore = constructionCompaniesInDb.Any(company =>
            company.UserCreatorId.Equals(constructionCompanyToCreate.UserCreatorId));

        if (userCreatedCompanyBefore)
        {
            throw new ObjectErrorServiceException("User has already created a company");
        }
    }

    #endregion

    #region Update Construction Company

    public void UpdateConstructionCompany(ConstructionCompany constructionCompanyWithUpdates)
    {
        try
        {
            CheckIfNameIsAlreadyUsed(constructionCompanyWithUpdates);

            ConstructionCompany constructionCompanyWithoutUpdates =
                _constructionCompanyRepository.GetConstructionCompanyById(constructionCompanyWithUpdates.Id);

            if (constructionCompanyWithoutUpdates is null)
            {
                throw new ObjectNotFoundServiceException();
            }

            MapProperties(constructionCompanyWithUpdates, constructionCompanyWithoutUpdates);

            constructionCompanyWithUpdates.ConstructionCompanyValidator();

            _constructionCompanyRepository.UpdateConstructionCompany(constructionCompanyWithUpdates);
        }
        catch (InvalidConstructionCompanyException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException)
        {
            throw;
        }
        catch (ObjectErrorServiceException)
        {
            throw;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    public ConstructionCompany GetConstructionCompanyByUserCreatorId(Guid ifOfUserCreator)
    {
        try
        {
            ConstructionCompany constructionCompanyFound =
                _constructionCompanyRepository.GetConstructionCompanyByUserCreatorId(ifOfUserCreator);

            if (constructionCompanyFound is null)
            {
                throw new ObjectNotFoundServiceException();
            }

            return constructionCompanyFound;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private void CheckIfNameIsAlreadyUsed(ConstructionCompany constructionCompanyToCreate)
    {
        IEnumerable<ConstructionCompany> constructionCompaniesInDb = GetAllConstructionCompanies();

        bool nameExists =
            constructionCompaniesInDb.Any(company => company.Name.Equals(constructionCompanyToCreate.Name));
        if (nameExists)
        {
            throw new ObjectErrorServiceException("Name already exists.");
        }
    }


    private static void MapProperties(ConstructionCompany constructionCompanyWithUpdates,
        ConstructionCompany constructionCompanyWithoutUpdates)
    {
        if (constructionCompanyWithUpdates.Equals(constructionCompanyWithoutUpdates))
        {
            throw new ObjectRepeatedServiceException();
        }

        foreach (PropertyInfo property in typeof(ConstructionCompany).GetProperties())
        {
            object? originalValue = property.GetValue(constructionCompanyWithoutUpdates);
            object? updatedValue = property.GetValue(constructionCompanyWithUpdates);

            if (Guid.TryParse(updatedValue?.ToString(), out Guid id))
            {
                if (id == Guid.Empty)
                {
                    property.SetValue(constructionCompanyWithUpdates, originalValue);
                }
            }

            if (updatedValue == null && originalValue != null)
            {
                property.SetValue(constructionCompanyWithUpdates, originalValue);
            }
        }
    }

    #endregion
}