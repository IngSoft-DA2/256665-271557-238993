import { Injectable } from '@angular/core';
import { LoginRequest } from '../interfaces/login-request';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../../environments/environment';
import { LoginResponse } from '../interfaces/login-response';
import { User } from '../interfaces/user';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private userConnected : User = {
    email : '',
    password : '',
    sessionString : '',
    userRole : ''
  };
  private userSubject = new BehaviorSubject<User>(this.userConnected);
  user$ = this.userSubject.asObservable();


  constructor(private http : HttpClient) { }

  login(loginRequest : LoginRequest) : Observable<LoginResponse>
  {
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/v2/sessions`,loginRequest)
  }

  storageUserValues(loginRequestValues : LoginRequest, responseOfApi: LoginResponse)
  {
    
    this.userConnected.email = loginRequestValues.email;
    this.userConnected.password = loginRequestValues.password;
    this.userConnected.sessionString = responseOfApi.sessionString;
    this.userConnected.userRole = responseOfApi.userRole;

    console.log(this.userConnected);
  }

  getUser(): Observable<User> {
    return this.user$;
  }

  removeUserConnected() : void 
  {
    const reInitUser : User = {
      email : '',
      password : '',
      sessionString : '',
      userRole : ''
    };

    const actualUser = this.userSubject.value;
    console.log(actualUser);

    this.userSubject.next(reInitUser)
    console.log(this.userSubject.value);
  }

}



