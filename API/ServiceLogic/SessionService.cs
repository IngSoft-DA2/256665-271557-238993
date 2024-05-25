using System.Security.Authentication;
using Domain;
using Domain.Enums;
using IDataAccess;
using IRepository;
using IServiceLogic;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class SessionService : ISessionService
{
    #region Constructor and Dependency Injection

    private SystemUser _currentUser;
    private readonly ISessionRepository _sessionRepository;
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IManagerRepository _managerRepository;
    private readonly IRequestHandlerRepository _requestHandlerRepository;


    public SessionService(ISessionRepository sessionRepository, IManagerRepository managerRepository,
        IAdministratorRepository administratorRepository, IRequestHandlerRepository requestHandlerRepository)
    {
        _sessionRepository = sessionRepository;
        _managerRepository = managerRepository;
        _administratorRepository = administratorRepository;
        _requestHandlerRepository = requestHandlerRepository;
    }

    #endregion

    #region Authenticate

    public Guid Authenticate(string email, string password)
    {
        try
        {
            IEnumerable<RequestHandler> requestHandlers = _requestHandlerRepository.GetAllRequestHandlers();
            IEnumerable<Manager> managers = _managerRepository.GetAllManagers();
            IEnumerable<Administrator> administrators = _administratorRepository.GetAllAdministrators();

            //cast entities to system user type
            List<SystemUser> users = new List<SystemUser>();
            foreach (var requestHandler in requestHandlers)
            {
                users.Add(requestHandler);
            }

            foreach (var manager in managers)
            {
                users.Add(manager);
            }

            foreach (var administrator in administrators)
            {
                users.Add(administrator);
            }

            //find user with matching email and password
            SystemUser user = users.FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user is null) throw new InvalidCredentialException("Invalid credentials");

            var session = new Session()
            {
                SessionString = Guid.NewGuid(),
                UserId = user.Id,
                UserRole = user.Role
            };
            _sessionRepository.CreateSession(session);

            return session.SessionString;
        }
        catch (InvalidCredentialException exceptionCaught)
        {
            throw new InvalidCredentialException(exceptionCaught.Message);
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    #endregion

    #region Logout

    public void Logout(Guid sessionId)
    {
        try
        {
            Session? session = _sessionRepository.GetSessionBySessionString(sessionId);
            if (session is null)
            {
                throw new ObjectNotFoundServiceException();
            }

            _sessionRepository.DeleteSession(session);
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

    #endregion
    
    #region Get user role by session string

    public SystemUserRoleEnum GetUserRoleBySessionString(Guid sessionStringOfUser)
    {
        try
        {
            Session? session = _sessionRepository.GetSessionBySessionString(sessionStringOfUser);

            if (session is null)
            {
                throw new ObjectNotFoundServiceException();
            }

            return session.UserRole;
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

    #endregion

    #region Is session valid

    public bool IsSessionValid(Guid sessionString)
    {
        try
        {
            Session? session = _sessionRepository.GetSessionBySessionString(sessionString);
            return session != null;
        }
        catch (Exception exceptionCaught)
        {
            throw new UnknownServiceException(exceptionCaught.Message);
        }
    }

    #endregion
}