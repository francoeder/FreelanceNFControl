import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  private userData = "USER-DATA";

  constructor() {}

  clean(): void {
    sessionStorage.clear();
    localStorage.clear();
  }

  public saveUserData(user: any) {
      var userStringfied = JSON.stringify(user);

      sessionStorage.removeItem(this.userData);
      sessionStorage.setItem(this.userData, userStringfied);
  }

  public getUserData(): any {
    const userStringfied = sessionStorage.getItem(this.userData);

    if (userStringfied) {
      return JSON.parse(userStringfied);
    }

    return null;
  }

  public isLoggedIn(): boolean {
    const user = sessionStorage.getItem(this.userData);
    if (user !== null) {
      return true;
    }

    return false;
  }

  public saveToSession(key: string, value: any) {
    var valueStringfied = JSON.stringify(value);

    sessionStorage.removeItem(key);
    sessionStorage.setItem(key, valueStringfied);
  }

  public getFromSession(key: string) {
    const valueStringfied = sessionStorage.getItem(key);

    if (valueStringfied) {
      return JSON.parse(valueStringfied);
    }

    return null;
  }
}