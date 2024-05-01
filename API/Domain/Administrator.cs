namespace Domain;

public class Administrator : Person
{
    #region Properties
    public string LastName { get; set; }
    public string Password { get; set; }
    
    #endregion


    public override bool Equals(object? objectToCompare)
    {
        Administrator? administratorToCompare = objectToCompare as Administrator;

        return Id == administratorToCompare.Id && Firstname == administratorToCompare.Firstname &&
               LastName == administratorToCompare.LastName && Email == administratorToCompare.Email;
    }

    #region Validators
    
    public void AdministratorValidator()
    {
        try
        {
            PersonValidator();
        }
        catch (InvalidPersonException exceptionCaught)
        {
            throw new InvalidAdministratorException(exceptionCaught.Message);
        }
        PasswordValidator();
        LastnameValidator();
    }

    private void LastnameValidator()
    {
        if (string.IsNullOrEmpty(LastName))
        {
            throw new InvalidAdministratorException("Last name is required");
        }
    }

    private void PasswordValidator()
    {
        if (string.IsNullOrEmpty(Password) || Password.Length < 8)
        {
            throw new InvalidAdministratorException("Password must have at least 8 characters");
        }
    }
    
    #endregion
}