import { Component } from '@angular/core';
import { GoogleAuthServiceService } from 'src/app/services/google-auth-service.service';
import { InternalAuthServiceService } from 'src/app/services/internal-auth-service.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  constructor(
    private gAuthService: GoogleAuthServiceService,
    private internalAuthService: InternalAuthServiceService
  ) {}

  login() {
    console.log('Clicked login');
    this.gAuthService.signInWithGoogle();
    this.gAuthService.socialUser$.subscribe((socialUser) => {
      console.log('\n\n\n ========= Logged in   ================\n');
      console.log(socialUser);
      console.log('\n\n\n========================\n');
      this.internalAuthService.loginWithGoogle({
        idToken: socialUser.idToken,
        provider: socialUser.provider,
      });
    });
  }
}
