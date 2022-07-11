import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { GoogleAuthServiceService } from '../services/google-auth-service.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardGuard implements CanActivate {
  constructor(
    private googleAuthService: GoogleAuthServiceService,
    private router: Router
  ) {}
  canActivate(): Observable<boolean> {
    return this.googleAuthService.socialUser$.pipe(
      map((user) => {
        if (user) return true;
        this.router.navigate(['/home']);
        return false;
      })
    );
  }
}
