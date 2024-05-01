namespace Domain;

public class Manager : Person
{
    public IEnumerable<Building> Buildings { get; set; } = new List<Building>();
    public IEnumerable<MaintenanceRequest> Requests { get; set; } = new List<MaintenanceRequest>();
    public string Password { get; set; }

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

    private void PasswordValidator()
    {
        if (string.IsNullOrEmpty(Password) || Password.Length < 8)
        {
            throw new InvalidManagerException("Password must have at least 8 characters");
        }
    }
}