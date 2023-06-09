import { Component } from '@angular/core';
import { GlobalVariblesService } from 'src/app/store/global-varibles.service';
import { HomePageService } from './home-page.service';

@Component({
  selector: 'app-home',
  templateUrl: './home-page.component.html',
})
export class HomePageComponent {

  isLoading = false
  isAddNewProjectLoading = false
  userData = JSON.parse(localStorage.getItem("user") + "").user
  projects: any[] = []
  newProjectName = ""
  localStorage = localStorage
  updatedName = ""
  isUpdateingProjectName = false
  selectedProjectId = ""

  errMsg = ""
  constructor(private homePageService: HomePageService, private store: GlobalVariblesService) {
    this.store.getErrMsg().subscribe((value) => {
      this.errMsg = value
    })
  }

  ngOnInit() {
    this.handleOnGetAllUserProjects()
  }

  handleOnGetAllUserProjects() {
    this.isLoading = true
    this.homePageService.handleOnGetAllProjects()
      .subscribe((res: any) => {
        this.projects = res.data.$values
        this.isLoading = false
      }, err => {
        this.store.setErrMsg(err.error.error)
      })
  }

  handleOnAddProject() {
    if (this.newProjectName) {
      this.isAddNewProjectLoading = true
      this.homePageService.handleOnAddNewProject(this.newProjectName).subscribe((data: any) => {
        this.projects.push(data.data)
        this.newProjectName = ""
        this.isAddNewProjectLoading = false
      }, err => {
        this.store.setErrMsg(err.error.error)
        this.isAddNewProjectLoading = false
      })
    }
  }

  jsonToString(json: any) {
    return JSON.stringify(json)
  }

  handleOnClickDelete(projectId: string) {
    this.isLoading = true
    const i = this.projects.findIndex(p => p.id === projectId)
    this.projects.splice(i, 1)
    this.homePageService.handleOnDeleteProject(projectId)
      .subscribe((res: any) => {
        this.isLoading = false
      }, (err: any) => {
        this.store.setErrMsg(err.error.error)
        this.isLoading = false
      })
  }
  handleOnClickUpdate() {
    if (this.updatedName) {

      this.isLoading = true
      const i = this.projects.findIndex(p => p.id === this.selectedProjectId)
      this.projects[i].name = this.updatedName
      // this.projects.splice(i ,1)
      this.homePageService.handleOnUpdateProject(this.selectedProjectId, this.updatedName)
        .subscribe((res: any) => {
          this.isUpdateingProjectName = false
          this.updatedName = ""
          this.isLoading = false
        }, (err: any) => {
          this.store.setErrMsg(err.error.error)
          this.isUpdateingProjectName = false
          this.updatedName = ""
          this.isLoading = false
        })
    }
  }
}
