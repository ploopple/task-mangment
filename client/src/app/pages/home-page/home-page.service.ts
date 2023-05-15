import { CookieService } from 'ngx-cookie-service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class HomePageService {
  headers = new HttpHeaders().set('Authorization', `Bearer ${this.cookieService.get("token")}`)
  userData: any
  constructor(private http: HttpClient, private cookieService: CookieService) { 
    this.userData = JSON.parse(localStorage.getItem("user")+"")
  }

  handleOnGetAllProjects() {
    return this.http.get('http://localhost:5242/api/Project/getAllUserProjects', {headers:this.headers})
  }
  handleOnAddNewProject(name: string) {
    return this.http.post('http://localhost:5242/api/Project', {name}, {headers:this.headers, responseType: "json"})
  }
  handleOnDeleteProject(projectId: string) {
    return this.http.delete('http://localhost:5242/api/Project/' + projectId, {headers:this.headers})
  }
  handleOnUpdateProject(projectId: string, newName: string) {
    return this.http.patch('http://localhost:5242/api/Project?projectId='  + projectId, {name: newName}, {headers:this.headers})
  }
}
