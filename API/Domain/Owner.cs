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
        if (string.IsNullOrEmpty(Firstname))
        {
            throw new InvalidOwnerException("Firstname is required");
        }

        if (string.IsNullOrEmpty(Lastname))
        {
            throw new InvalidOwnerException("Lastname is required");
        }
        
        // Pattern has all the available letters and digits that an email can have
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        //Regex checks if the email has the correct pattern and so on if the email is not empty
        bool hasCorrectPattern = Regex.IsMatch(Email, pattern);

        if (!hasCorrectPattern)
        {
            throw new InvalidOwnerException("Error on email pattern");
        }
        
        if (!Firstname.All(char.IsLetter))
        {
            throw new InvalidOwnerException("Firstname cannot contain special characters.");
        }
    }


    public override bool Equals(object? objectToCompare)
    {
        Owner? ownerToCompare = objectToCompare as Owner;
        if (ownerToCompare is null) return false;

        return Id == ownerToCompare.Id && Firstname == ownerToCompare.Firstname &&
               Lastname == ownerToCompare.Lastname && Email == ownerToCompare.Email;
    }
}