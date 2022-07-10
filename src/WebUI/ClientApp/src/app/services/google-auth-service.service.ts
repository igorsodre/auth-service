import {
  GoogleLoginProvider,
  SocialAuthService,
  SocialUser,
} from '@abacritt/angularx-social-login';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class GoogleAuthServiceService {
  private extAuthChangeSub = new Subject<SocialUser>();
  public extAuthChanged = this.extAuthChangeSub.asObservable();
  constructor(
    private http: HttpClient,
    private externalAuthService: SocialAuthService
  ) {
    this.externalAuthService.authState.subscribe((user) => {
      this.extAuthChangeSub.next(user);
    });
  }

  public signInWithGoogle = () => {
    this.externalAuthService.signIn(GoogleLoginProvider.PROVIDER_ID);
  };

  public signOutExternal = () => {
    this.externalAuthService.signOut();
  };
}
