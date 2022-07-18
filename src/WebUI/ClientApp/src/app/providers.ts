import { environment } from './../environments/environment';
import {
  GoogleLoginProvider,
  SocialAuthServiceConfig,
} from '@abacritt/angularx-social-login';
import { Provider } from '@angular/core';
import { ManageHttpInterceptor } from './interceptors/manage-http.interceptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpCancelService } from './services/http-cancel.service';

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

const httpInteruptCancelledCalls = {
  provide: HTTP_INTERCEPTORS,
  useClass: ManageHttpInterceptor,
  multi: true,
};

const providers: Provider[] = [
  HttpCancelService,
  socialLoginProvider,
  httpInteruptCancelledCalls,
];

export default providers;
