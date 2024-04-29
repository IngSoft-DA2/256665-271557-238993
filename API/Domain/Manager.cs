namespace Domain;

public class Manager : Person
{
    public IEnumerable<Guid> Buildings { get; set; }
    public string Password { get; set; }

    public void PasswordValidator()
    {
        if (string.IsNullOrEmpty(Password) || Password.Length < 8)
        {
            throw new InvalidManagerException("Password must have at least 8 characters");
        }
    }
    
    public void BuildingValidator()
    {
        if (Buildings == null)
        {
            throw new InvalidManagerException("Manager must have a list of buildings");
        }
    }
}