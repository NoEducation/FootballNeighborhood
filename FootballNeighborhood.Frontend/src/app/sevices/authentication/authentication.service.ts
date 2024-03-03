import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { RegisterUserRequest } from 'src/app/login/models/register-user-request.model';
import { UserCredentials } from 'src/app/login/models/user-credentials.model';
import { UserLogged } from 'src/app/login/models/user-logged.model';
import { OperationResult } from 'src/app/models/infrastructure/operation-result.model';
import { SuccessMessageAndObjectId } from 'src/app/models/infrastructure/success-message-and-object-id.model';
import { environment } from 'src/environments/environment';


@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  authenticationLink = environment.apiUrl + 'authentication/';

  readonly helper : JwtHelperService;

  constructor(private http: HttpClient) {
    this.helper = new JwtHelperService();
  }

  login(userCredentials: UserCredentials): Observable<OperationResult<UserLogged>> {
    return this.http.post<OperationResult<UserLogged>>(this.authenticationLink + 'login', userCredentials);
  }

  register(registerUserRequest: RegisterUserRequest) : Observable<OperationResult<SuccessMessageAndObjectId>>{
    return this.http.post<OperationResult<SuccessMessageAndObjectId>>(this.authenticationLink + 'register', registerUserRequest);
  }

  isAuthenticated(): boolean {
    const token = sessionStorage.getItem('token');
    return !this.helper.isTokenExpired(token);
  }

  getUserId() : number{
    const userId = sessionStorage.getItem('userId');

    if(!userId) throw new Error('User is not logged');

    return +userId;
  }
}
