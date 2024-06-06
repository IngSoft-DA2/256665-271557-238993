import { Injectable } from '@angular/core';
import { LoginRequest } from '../interfaces/login-request';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { LoginResponse } from '../interfaces/login-response';
import { User } from '../interfaces/user';
import { SystemUserRoleEnum } from '../../invitation/interfaces/enums/system-user-role-enum';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private userConnected: User | undefined = undefined;

  private userSubject = new BehaviorSubject<User | undefined>(this.userConnected);
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient) { }

  login(loginRequest: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/v2/sessions`, loginRequest);
  }

  storageUserValues(loginRequestValues: LoginRequest, responseOfApi: LoginResponse) {
    alert(responseOfApi.userRole);
    this.userConnected = {
      email: loginRequestValues.email,
      password: loginRequestValues.password,
      sessionString: responseOfApi.sessionString,
      userRole: this.getSystemUserRole(responseOfApi.userRole)
    };


    this.userSubject.next(this.userConnected);
    console.log(this.userConnected);
  }

  getUser(): Observable<User | undefined> {
    return this.user$;
  }

  removeUserConnected(): void {
    this.userConnected = undefined;
    this.userSubject.next(this.userConnected);
  }

  getSystemUserRole(role: string): SystemUserRoleEnum {
    alert(role);
    switch (role.toLowerCase()) {
      case 'admin':
        return SystemUserRoleEnum.Admin;
      case 'manager':
        return SystemUserRoleEnum.Manager;
      case 'requestHandler':
        return SystemUserRoleEnum.RequestHandler;
      case 'constructioncompanyadmin':
        return SystemUserRoleEnum.ConstructionCompanyAdmin;

      default:
        return SystemUserRoleEnum.Unknown;
    }
  }
}
