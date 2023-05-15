import { CookieService } from 'ngx-cookie-service';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { GlobalVariblesService } from '../store/global-varibles.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private loggedIn = localStorage.getItem("user") !== "" && this.cookieService.get("token") !== "";
  private isLoggedin = new BehaviorSubject<boolean>(localStorage.getItem("user") !== "" && this.cookieService.get("token") !== "")

  constructor(private http: HttpClient, private cookieService: CookieService, private router: Router, private store: GlobalVariblesService) { }

  getUserInfo() {
    this.http.get('http://localhost:5242/api/User', {
      headers: { "Authorization": "Bearer " + this.cookieService.get("token") }, responseType: "json"
    }
    ).subscribe((res: any) => {
      localStorage.setItem("user", JSON.stringify({ user: res.data }))
      this.loggedIn = true;
      this.isLoggedin.next(true)
      if (this.router.url === "/login" || this.router.url === "/singup") {
        this.router.navigate([''])
        location.reload
      }
    }, err => {
      this.cookieService.delete("token")
      localStorage.removeItem("user")
      localStorage.removeItem("project")
      this.isLoggedin.next(false)
      this.loggedIn = false;
      this.router.navigate(["login"])
    })
  }

  singup(username: string, email: string, password: string) {
    return this.http.post("http://localhost:5242/api/User", {
      "username": username,
      "email": email,
      "password": password
    })
  }

  login(email: string, password: string) {
    return this.http.post("http://localhost:5242/api/User/login", {
      "username": "string",
      "email": email,
      "password": password
    })
  }

  logout() {
    this.cookieService.delete("token")
    localStorage.removeItem("user")
    localStorage.removeItem("project")
    this.isLoggedin.next(false)
    this.loggedIn = false;
    this.router.navigate(["login"])
  }

  isLoggedIn() {
    return this.loggedIn;
  }
  isLoggedIns() {
    return this.isLoggedin.asObservable();
  }
}
