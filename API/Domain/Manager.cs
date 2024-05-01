namespace Domain;

public class Manager : SystemUser
{
    public IEnumerable<Building> Buildings { get; set; } = new List<Building>();

    public void ManagerValidator()
    {
        PersonValidator();
        PasswordValidator();
    }

    public override bool Equals(object? objectToCompare)
    {
        Manager managerToCompare = objectToCompare as Manager;
        return managerToCompare.Password == managerToCompare.Password &&
               Buildings.SequenceEqual(managerToCompare.Buildings);
    }
}