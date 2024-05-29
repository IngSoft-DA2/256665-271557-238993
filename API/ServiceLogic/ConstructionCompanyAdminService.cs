using System.Diagnostics.CodeAnalysis;
using Domain;
using Domain.CustomExceptions;
using Domain.Enums;
using IDataAccess;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class ConstructionCompanyAdminService : IConstructionCompanyAdminService
{
    #region Constructor and Dependency Injection

    private readonly IConstructionCompanyAdminRepository _constructionCompanyAdminRepository;
    private readonly IInvitationService _invitationService;

    public ConstructionCompanyAdminService(IConstructionCompanyAdminRepository constructionCompanyAdminRepository,
        IInvitationService invitationService)
    {
        _constructionCompanyAdminRepository = constructionCompanyAdminRepository;
        _invitationService = invitationService;
    }

    #endregion

    #region Create Construction Company Admin

    public void CreateConstructionCompanyAdminByInvitation(ConstructionCompanyAdmin constructionCompanyAdminToCreate,
        Guid? invitationId)
    {
        if (invitationId is null) throw new ObjectErrorServiceException(
            "Invitation id is required unless user role is ConstructionCompanyAdmin");
        
        ValidationsForBeingPossibleToCreate(constructionCompanyAdminToCreate);
        MarkInvitationAsValid(invitationId.Value);
        
        try
        {
            _constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(constructionCompanyAdminToCreate);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }


    public void CreateConstructionCompanyAdminForAdmins(ConstructionCompanyAdmin constructionCompanyAdminToCreate)
    {
        ValidationsForBeingPossibleToCreate(constructionCompanyAdminToCreate);
        try
        {
            _constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(constructionCompanyAdminToCreate);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    #region Auxiliary Functions

    private void ValidationsForBeingPossibleToCreate(ConstructionCompanyAdmin constructionCompanyAdminToCreate)
    {
        try
        {
            constructionCompanyAdminToCreate.ConstructionCompanyAdminValidator();

            IEnumerable<ConstructionCompanyAdmin> allConstructionCompanyAdmins =
                _constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins();
            CheckIfEmailIsAlreadyRegistered(constructionCompanyAdminToCreate, allConstructionCompanyAdmins);
        }
        catch (InvalidConstructionCompanyAdminException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private void CheckIfEmailIsAlreadyRegistered(ConstructionCompanyAdmin constructionCompanyAdminToCheck,
        IEnumerable<ConstructionCompanyAdmin> constructionCompanyAdmins)
    {
        if (constructionCompanyAdmins.Any(constructionCompanyAdmin =>
                constructionCompanyAdmin.Email == constructionCompanyAdminToCheck.Email))
        {
            throw new ObjectRepeatedServiceException();
        }
    }

    private void MarkInvitationAsValid(Guid invitationId)
    {
        try
        {
            Invitation invitationToAccept = _invitationService.GetInvitationById(invitationId);
            Invitation invitationAccepted = AcceptInvitation(invitationToAccept);
            _invitationService.UpdateInvitation(invitationAccepted.Id, invitationAccepted);
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundServiceException();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    private Invitation AcceptInvitation(Invitation invitationToAccept)
    {
        Invitation invitationAccepted = new Invitation
        {
            Id = invitationToAccept.Id,
            Status = StatusEnum.Accepted,
            ExpirationDate = invitationToAccept.ExpirationDate
        };
        return invitationAccepted;
    }

    #endregion

    #endregion

    #region Get All Construction Company Admins

    public IEnumerable<ConstructionCompanyAdmin> GetAllConstructionCompanyAdmins()
    {
        try
        {
            return _constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins();
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    #endregion
}