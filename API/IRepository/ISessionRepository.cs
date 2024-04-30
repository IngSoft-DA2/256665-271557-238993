namespace IRepository;

public interface ISessionRepository
{
    bool SessionExists(Guid headerValidationString);
}