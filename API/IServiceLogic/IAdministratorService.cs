using Domain;

namespace IServiceLogic;

public interface IAdministratorService
{
    public Administrator CreateAdministrator(Administrator administratorToCreate);
}