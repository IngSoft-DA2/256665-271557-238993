using System.Security.Authentication;
using Domain;
using IRepository;
using IServiceLogic;

namespace ServiceLogic;

public class SessionService : ISessionService
{
    #region Constructor and Dependency Injection

    private SystemUser _currentUser;
    private readonly ISessionRepository _sessionRepository;
    private readonly IAdministratorRepository _administratorRepository;
    private readonly IManagerRepository _managerRepository;
    private readonly IRequestHandlerRepository _requestHandlerRepository;


    public SessionService(ISessionRepository sessionRepository,
        IManagerRepository managerRepository,
        IAdministratorRepository administratorRepository,
        IRequestHandlerRepository requestHandlerRepository)
    {
        _sessionRepository = sessionRepository;
        _managerRepository = managerRepository;
        _administratorRepository = administratorRepository;
        _requestHandlerRepository = requestHandlerRepository;
    }

    #endregion


    public Guid Authenticate(string email, string password)
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

        if (user == null)
            throw new InvalidCredentialException("Invalid credentials");

        var session = new Session()
        {
            User = user,
        };
        _sessionRepository.CreateSession(session);
        _sessionRepository.Save();

        return session.SessionString;
    }

    public void Logout(Guid sessionId)
    {
        Session session = _sessionRepository.GetSessionById(sessionId);
        _sessionRepository.DeleteSession(session);
        _sessionRepository.Save();
    }

    public SystemUser? GetCurrentUser(Guid? token = null)
    {
        if (token is null) throw new ArgumentException("Cant retrieve user without it's token");

        var session = _sessionRepository.GetSessionById(token.Value);
        return session.User;
    }
}