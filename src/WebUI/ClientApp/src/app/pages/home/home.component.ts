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
  ) {
    this.setupLogin();
  }

  setupLogin() {
    this.gAuthService.socialUser$.subscribe((socialUser) => {
      this.internalAuthService
        .loginWithGoogle({
          idToken: socialUser.idToken,
          provider: socialUser.provider,
        })
        .subscribe((response) => {
          console.log('\n===Loging response\n');
          console.log(response);
        });
    });
  }

  async login() {
    await this.gAuthService.signInWithGoogle();
  }
}
