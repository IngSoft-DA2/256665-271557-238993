using Domain;

namespace IRepository;

public interface IManagerRepository
{
    public IEnumerable<Manager> GetAllManagers();
}