using Domain;
using IRepository;
using ServiceLogic.CustomExceptions;

namespace ServiceLogic;

public class AdministratorService : IAdministratorRepository
{

    private readonly IAdministratorRepository _administratorRepository;

    public AdministratorService(IAdministratorRepository administratorRepository)
    {
        _administratorRepository = administratorRepository;
    }

    public void CreateAdministrator(Administrator administratorToAdd)
    {
        _administratorRepository.CreateAdministrator(administratorToAdd);
    }
}