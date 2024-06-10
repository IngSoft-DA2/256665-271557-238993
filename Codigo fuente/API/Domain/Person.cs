using System.Text.RegularExpressions;

namespace Domain;
public abstract class Person
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string Email { get; set; }
    
    public virtual void PersonValidator()
    {
        ValidatingNameAspects();
        ValidatingCorrectPatternEmail();
    }

    private void ValidatingNameAspects()
    {
        ValidateName(Firstname, "Firstname");
    }

    protected virtual void ValidateName(string name, string fieldName)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new InvalidPersonException($"{fieldName} is required");
        }

        if (!name.All(char.IsLetter))
        {
            throw new InvalidPersonException($"{fieldName} cannot contain special characters.");
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
            throw new InvalidPersonException("Error on email pattern");
        }
    }
}