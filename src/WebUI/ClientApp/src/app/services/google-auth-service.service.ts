import {
  GoogleLoginProvider,
  SocialAuthService,
  SocialUser,
} from '@abacritt/angularx-social-login';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GoogleAuthServiceService {
  private extAuthChangeSub = new Subject<SocialUser>();
  public socialUser$ = this.extAuthChangeSub.asObservable();
  constructor(private externalAuthService: SocialAuthService) {
    this.externalAuthService.authState.subscribe((user) => {
      this.extAuthChangeSub.next(user);
    });
  }

  public signInWithGoogle = () => {
    return this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  };

  public signOutExternal = () => {
    return this.externalAuthService.signOut();
  };
}
