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
        Guid invitationId)
    {
        try
        {
            constructionCompanyAdminToCreate.ConstructionCompanyAdminValidator();

            IEnumerable<ConstructionCompanyAdmin> allConstructionCompanyAdmins =
                _constructionCompanyAdminRepository.GetAllConstructionCompanyAdmins();

            CheckIfEmailIsAlreadyRegistered(constructionCompanyAdminToCreate,allConstructionCompanyAdmins);

            Invitation invitationToAccept = _invitationService.GetInvitationById(invitationId);
            Invitation invitationAccepted = AcceptInvitation(invitationToAccept);
            _invitationService.UpdateInvitation(invitationAccepted.Id, invitationAccepted);

            _constructionCompanyAdminRepository.CreateConstructionCompanyAdmin(constructionCompanyAdminToCreate);
        }
        
        catch (InvalidConstructionCompanyAdminException exceptionCaught)
        {
            throw new ObjectErrorServiceException(exceptionCaught.Message);
        }
        catch (ObjectRepeatedServiceException)
        {
            throw new ObjectRepeatedServiceException();
        }
    }

    public void CreateConstructionCompanyAdminByAnotherAdmin(ConstructionCompanyAdmin constructionCompanyAdminToCreate)
    {
        throw new NotImplementedException();
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