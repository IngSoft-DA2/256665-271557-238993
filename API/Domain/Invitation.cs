using Domain.Enums;

namespace Domain;

public class Invitation
{
    public Guid Id = Guid.NewGuid();
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public DateTime ExpirationDate { get; set; }
    public StatusEnum Status { get; set; }


    public void InvitationValidator()
    {
        FirstnameValidation();
        LastnameValidation();
        EmailValidation();
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
    
    private void EmailValidation()
    {
        if (String.IsNullOrEmpty(Email))
        {
            throw new InvalidInvitationException("Email is required");
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