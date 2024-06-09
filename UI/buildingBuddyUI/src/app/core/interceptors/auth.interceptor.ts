import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { LoginService } from "../../features/login/services/login.service";



@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  sessionString: string = '';

  constructor(private loginService: LoginService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (this.shouldInterceptRequest(request)) {

      this.loginService.getUser()
        .subscribe({
          next: (Response) => {
            this.sessionString = Response?.sessionString ?? ''
          }
        });

      const authRequest = request.clone({
        setHeaders: {
          'Authorization': this.sessionString
        }
      });
    return next.handle(authRequest);
  }
   return next.handle(request);
  }


  private shouldInterceptRequest(request: HttpRequest<any>): boolean{
    return request.urlWithParams.indexOf('addAuth=true', 0) > -1 ? true : false;
    }
  


}

 
