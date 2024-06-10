namespace Domain;

public class Manager : SystemUser
{
    public IEnumerable<Building> Buildings { get; set; } = new List<Building>();
    public IEnumerable<MaintenanceRequest> Requests { get; set; } = new List<MaintenanceRequest>();

    public void ManagerValidator()
    {
        PersonValidator();
        PasswordValidator();
    }


    protected override void ValidateName(string name, string fieldName)
    {
    }

    
    public override bool Equals(object? objectToCompare)
    {
        Manager managerToCompare = objectToCompare as Manager;
        return managerToCompare.Password == managerToCompare.Password &&
               Buildings.SequenceEqual(managerToCompare.Buildings) &&
                Requests.SequenceEqual(managerToCompare.Requests);
    }
}