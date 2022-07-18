import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { ApiResponse } from '../models/responses/api';
import { ExternalAuthRequest } from './../models/requests/auth';
import { LoginResponse } from './../models/responses/auth';

@Injectable({
  providedIn: 'root',
})
export class InternalAuthServiceService {
  constructor(
    private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string
  ) {}

  loginWithGoogle(request: ExternalAuthRequest) {
    return this.http.post<ApiResponse<LoginResponse>>(
      this.baseUrl + 'api/auth/external',
      request
    );
  }
}
