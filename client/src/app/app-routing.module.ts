import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from './pages/login-page/login-page.component';
import { SingupPageComponent } from './pages/singup-page/singup-page.component';
import { NotFoundPageComponent } from './pages/not-found-page/not-found-page.component';
import { AuthGuard, AuthNotGuard } from './guards/auth.guard';
import { ProjectPageComponent } from './pages/project-page/project-page.component';
import { HomePageComponent } from './pages/home-page/home-page.component';

const routes: Routes = [
  { path: '', component: HomePageComponent, canActivate: [AuthGuard]},
  { path: 'login', component: LoginPageComponent, canActivate: [AuthNotGuard]},
  { path: 'singup', component: SingupPageComponent, canActivate: [AuthNotGuard]},
  { path: 'project/:id', component: ProjectPageComponent, canActivate: [AuthGuard]},
  { path: '**', component: NotFoundPageComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
