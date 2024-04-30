namespace Domain;

public class Manager : Person
{
    public IEnumerable<Building> Buildings { get; set; }
    public string Password { get; set; }

    public void ManagerValidator()
    {
        PersonValidator();
        PasswordValidator();
        BuildingValidator();
    }

    public override bool Equals(object? objectToCompare)
    {
        Manager managerToCompare = objectToCompare as Manager;
        if (managerToCompare is null) return false;

        return base.Equals(managerToCompare) && Password == managerToCompare.Password &&
               Buildings.SequenceEqual(managerToCompare.Buildings);
    }

    private void PasswordValidator()
    {
        if (string.IsNullOrEmpty(Password) || Password.Length < 8)
        {
            throw new InvalidManagerException("Password must have at least 8 characters");
        }
    }

    private void BuildingValidator()
    {
        if (Buildings == null)
        {
            throw new InvalidManagerException("Manager must have a list of buildings");
        }
    }
}