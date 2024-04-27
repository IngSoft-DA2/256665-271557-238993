using System.Text.RegularExpressions;
using Domain.Enums;

namespace Domain;

public class Invitation
{
    public Guid Id = Guid.NewGuid();
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public DateTime ExpirationDate { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Pending;


    public void InvitationValidator()
    {
        FirstnameValidation();
        LastnameValidation();
        EmailValidation();
        ExpirationDateValidation();
    }


    private void FirstnameValidation()
    {
        if (String.IsNullOrEmpty(Firstname))
        {
            throw new InvalidInvitationException("Firstname is required");
        }

        if (!Firstname.All(char.IsLetter))
        {
            throw new InvalidInvitationException("Firstname cannot contain special characters.");
        }
    }

    private void LastnameValidation()
    {
        if (String.IsNullOrEmpty(Lastname))
        {
            throw new InvalidInvitationException("Lastname is required");
        }

        if (!Lastname.All(char.IsLetter))
        {
            throw new InvalidInvitationException("Firstname cannot contain special characters.");
        }
    }

    private void EmailValidation()
    {
        // Pattern has all the available letters and digits that an email can have
        const string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

        //Regex checks if the email has the correct pattern and so on if the email is not empty
        bool hasCorrectPattern = Regex.IsMatch(Email, pattern);

        if (!hasCorrectPattern)
        {
            throw new InvalidInvitationException("Error on email pattern");
        }
    }

    private void ExpirationDateValidation()
    {
        if (ExpirationDate <= DateTime.Today)
        {
            throw new InvalidInvitationException("Expiration date must be greater than today");
        }
    }
    
    public override bool Equals(object? obj)
    {
        Invitation? invitationToCompare = obj as Invitation;
        if (invitationToCompare == null)
        {
            return false;
        }

        return Id == invitationToCompare.Id && Firstname == invitationToCompare.Firstname &&
               Lastname == invitationToCompare.Lastname && Email == invitationToCompare.Email &&
               ExpirationDate == invitationToCompare.ExpirationDate && Status == invitationToCompare.Status;
    }
}