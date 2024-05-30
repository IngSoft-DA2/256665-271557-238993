using System.Text.RegularExpressions;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain;

public class Invitation
{
    public Guid Id { get; set; }
    public string Firstname { get; set; }
    public string? Lastname { get; set; }
    public string Email { get; set; }
    public DateTime ExpirationDate { get; set; }
    public StatusEnum Status { get; set; } = StatusEnum.Pending;
    public SystemUserRoleEnum Role { get; set; }

    public void InvitationValidator()
    {
        FirstnameValidation();
        LastnameValidation();
        EmailValidation();
        ExpirationDateValidation();
        RoleValidator();
    }

    public override bool Equals(object? obj)
    {
        Invitation? invitationToCompare = obj as Invitation;
        return Id == invitationToCompare.Id && Firstname == invitationToCompare.Firstname &&
               Lastname == invitationToCompare.Lastname && Email == invitationToCompare.Email &&
               ExpirationDate == invitationToCompare.ExpirationDate && Status == invitationToCompare.Status &&
               Role == invitationToCompare.Role;
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
        if (Role.Equals(SystemUserRoleEnum.ConstructionCompanyAdmin))
        {
            if (String.IsNullOrEmpty(Lastname))
            {
                throw new InvalidInvitationException("Lastname is required");
            }

            if (!Lastname.All(char.IsLetter))
            {
                throw new InvalidInvitationException("Lastname cannot contain special characters.");
            }
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

    private void RoleValidator()
    {
        if (Role != SystemUserRoleEnum.Manager && Role != SystemUserRoleEnum.ConstructionCompanyAdmin)
        {
            throw new InvalidInvitationException("Role must be Manager or Construction Company Administrator");
        }
    }
}