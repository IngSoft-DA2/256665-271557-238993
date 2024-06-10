import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { SystemUserRoleEnum } from '../../features/invitation/interfaces/enums/system-user-role-enum';
import { LoginService } from '../../features/login/services/login.service';


@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private loginService: LoginService, private router: Router) {}

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    const roles: SystemUserRoleEnum[] = next.data['roles']; 

    return this.loginService.getUser().pipe(
      map(user => {
        console.log(roles);
        console.log(user?.userRole)
        if (user && (!roles || roles.includes(user.userRole as SystemUserRoleEnum))) {
          return true;
        } else {
          return this.router.createUrlTree(['/home']);
        }
      })
    );
  }
}
