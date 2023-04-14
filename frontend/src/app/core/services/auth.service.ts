import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private loginApiUrl = `${environment.apiUrl}/user/login`;
  private registerApiUrl = `${environment.apiUrl}/user`;

  constructor(
    private http: HttpClient,
  ) { }

  login(email: string, password: string): Observable<any> {
    const body = {
      userNameOrEmail: email,
      password: password
    }

    return this.http.post(
      this.loginApiUrl,
      body,
      httpOptions
    );
  }

  register(name: string, lastName: string, email: string, password: string, confirmPassword: string): Observable<any> {
    var body = {
      firstName: name,
      lastName: lastName,
      userName: email,
      email: email,
      password: password,
      passwordConfirmation: confirmPassword
    };

    return this.http.post(
      this.registerApiUrl,
      body,
      httpOptions
    );
  }

}