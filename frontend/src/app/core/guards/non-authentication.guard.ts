import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { StorageService } from '../services/storage.service';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class NonAuthenticationGuard implements CanActivate {

  constructor(
    private router: Router,
    private storageService: StorageService,
    private messageService: MessageService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      var isLoggedIn = this.storageService.isLoggedIn();

      if (isLoggedIn) {
        this.router.navigate(['/main']);
      }
      
      return !isLoggedIn;
  }
  
}
