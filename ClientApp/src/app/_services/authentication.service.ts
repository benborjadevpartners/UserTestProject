import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { map } from 'rxjs/operators';


@Injectable()
export class AuthenticationService {
    constructor(private http: HttpClient) { }

  private loggedIn = new BehaviorSubject<boolean>(false);

  login(username: string, password: string) {

    return this.http.post('/login', { Username: username, Password: password }).pipe(map(user => {
      // login successful if there's a jwt token in the response
      if (user) {
        // store user details and jwt token in local storage to keep user logged in between page refreshes
        localStorage.setItem('currentUser', JSON.stringify(user));

        this.loggedIn.next(true);
      }

      return user;
    }));

        
  }

  get isLoggedIn() {
    return this.loggedIn.asObservable(); 
  }

  logout() {
      // remove user from local storage to log user out
    localStorage.removeItem('currentUser');
    this.loggedIn.next(false);
  }
}
