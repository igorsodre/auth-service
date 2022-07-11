import { environment } from './../environments/environment.prod';
import {
  GoogleLoginProvider,
  SocialAuthServiceConfig,
} from '@abacritt/angularx-social-login';
import { Provider } from '@angular/core';

const socialLoginProvider = {
  provide: 'SocialAuthServiceConfig',
  useValue: {
    autoLogin: false,
    providers: [
      {
        id: GoogleLoginProvider.PROVIDER_ID,
        provider: new GoogleLoginProvider(environment.googleClientId),
      },
    ],
    onError: (err) => {
      console.error(err);
    },
  } as SocialAuthServiceConfig,
};

const providers: Provider[] = [socialLoginProvider];

export default providers;
