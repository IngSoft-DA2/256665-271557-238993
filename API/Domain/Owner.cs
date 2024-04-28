using System.Text.RegularExpressions;

namespace Domain;

public class Owner
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }

    public IEnumerable<Flat> Flats { get; set; } = new List<Flat>();


    public void OwnerValidator()
    {
        ValidatingFirstnameAndLastnameAspects();
        ValidatingCorrectPatternEmail();
    }
    public override bool Equals(object? objectToCompare)
    {
        Owner? ownerToCompare = objectToCompare as Owner;
        if (ownerToCompare is null) return false;

        return Id == ownerToCompare.Id && Firstname == ownerToCompare.Firstname &&
               Lastname == ownerToCompare.Lastname && Email == ownerToCompare.Email
               && Flats.SequenceEqual(ownerToCompare.Flats);
    }

    private void ValidatingFirstnameAndLastnameAspects()
    {
        ValidateName(Firstname, "Firstname");
        ValidateName(Lastname, "Lastname");
    }
    private void ValidateName(string name, string fieldName)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new InvalidOwnerException($"{fieldName} is required");
        }

        if (!name.All(char.IsLetter))
        {
            throw new InvalidOwnerException($"{fieldName} cannot contain special characters.");
        }
    }
    private void ValidatingCorrectPatternEmail()
    {
        // Pattern has all the available letters and digits that an email can have
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        //Regex checks if the email has the correct pattern and so on if the email is not empty
        bool hasCorrectPattern = Regex.IsMatch(Email, pattern);

        if (!hasCorrectPattern)
        {
            throw new InvalidOwnerException("Error on email pattern");
        }
    }
    
}