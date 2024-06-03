import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { InvitationListComponent } from './features/invitation/invitation-list/invitation-list.component';
import { InvitationUpdateComponent } from './features/invitation/invitation-update/invitation-update.component';
import { InvitationCreateComponent } from './features/invitation/invitation-create/invitation-create.component';

const routes: Routes =
[

  {
    path: 'invitations/list',
    component: InvitationListComponent
  },

  {
    path: 'invitations/update/:id',
    component: InvitationUpdateComponent
  },
  {
    path : 'invitations/create',
    component : InvitationCreateComponent
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule 
{ 
}
