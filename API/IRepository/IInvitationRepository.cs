﻿using Domain;

namespace IRepository;

public interface IInvitationRepository
{
    public IEnumerable<Invitation> GetAllInvitations();
    public Invitation GetInvitationById(Guid invitationId);
    public void CreateInvitation(Invitation invitationToAdd);
    public void UpdateInvitation(Invitation invitationUpdated);
}