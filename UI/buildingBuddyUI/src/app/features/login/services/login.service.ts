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

  private userConnected: User | undefined = undefined;
  private userSubject = new BehaviorSubject<User | undefined>(this.getUserFromLocalStorage());
  user$ = this.userSubject.asObservable();

  constructor(private http: HttpClient) { }

  login(loginRequest: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${environment.apiBaseUrl}/api/v2/sessions`, loginRequest);
  }

  storageUserValues(loginRequestValues: LoginRequest, responseOfApi: LoginResponse) {
    this.userConnected = {
      userId: responseOfApi.userId,
      email: loginRequestValues.email,
      password: loginRequestValues.password,
      sessionString: responseOfApi.sessionString,
      userRole: responseOfApi.userRole
    };

    this.userSubject.next(this.userConnected);
    localStorage.setItem('user', JSON.stringify(this.userConnected)); 
    console.log(this.userConnected);
  }

  getUser(): Observable<User | undefined> {
    return this.user$;
  }

  logout(): Observable<void> {
    return this.http.delete<void>(`${environment.apiBaseUrl}/api/v2/sessions?addAuth=true`);
  }

  removeUserConnected(): void {
    this.logout()
      .subscribe({
        next: () => {
          this.userConnected = undefined;
          this.userSubject.next(this.userConnected);
          localStorage.removeItem('user'); 
        }
      });
  }

  private getUserFromLocalStorage(): User | undefined {
    const userJson = localStorage.getItem('user');
    return userJson ? JSON.parse(userJson) : undefined;
  }
}
