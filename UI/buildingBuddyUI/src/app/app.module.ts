import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { InvitationListComponent } from './features/invitation/invitation-list/invitation-list.component';
import { HttpClientModule } from '@angular/common/http';
import { InvitationUpdateComponent } from './features/invitation/invitation-update/invitation-update.component';
import { InvitationCreateComponent } from './features/invitation/invitation-create/invitation-create.component';
import { InvitationListByEmailComponent } from './features/invitation/invitation-list-by-email/invitation-list-by-email.component';
import { ManagerCreateComponent } from './features/manager/features/manager-create/manager-create.component';


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    InvitationListComponent,
    InvitationUpdateComponent,
    InvitationCreateComponent,
    InvitationListByEmailComponent,
    ManagerCreateComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
