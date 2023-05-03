import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';


const helper = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  authenticationLink = environment.apiUrl + 'authentication/';

  constructor(private http: HttpClient) { }

  authenticate(username: string, password: string): Observable<OperationResult<UserAuthenticated>> {
    const authenticationData = JSON.stringify({ username, password, isappuser: true });
    return this.http.post<OperationResult<UserAuthenticated>>(this.authenticationLink + 'authenticate/', authenticationData);
  }

  isAuthenticated(): boolean {
    const token = sessionStorage.getItem('token');
    return !helper.isTokenExpired(token);
  }

  getAppConfiguration(): Observable<object> {
    return this.http.get(this.authenticationLink + 'getConfiguration/');
  }
}
