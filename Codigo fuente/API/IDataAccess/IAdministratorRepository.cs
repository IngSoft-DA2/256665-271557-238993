using Domain;

namespace IRepository;

public interface IAdministratorRepository
{
    public void CreateAdministrator(Administrator administratorToAdd);
    public IEnumerable<Administrator> GetAllAdministrators();
}