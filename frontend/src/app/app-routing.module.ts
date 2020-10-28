import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { MainComponent } from './main/main.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './utilities/AuthGuard';
import { UnitTypeWindowComponent } from './unit-type-window/unit-type-window.component';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent},
  { path: '', component: MainComponent, canActivate: [AuthGuard], children: [
    {path: 'units', component: UnitTypeWindowComponent}
  ]},

  // Default redirect to main
  { path: '**', redirectTo: ''}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
