import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatCardModule } from '@angular/material/card'
import { FlexLayoutModule } from '@angular/flex-layout'
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatInputModule } from '@angular/material/input'
import { MatButtonModule } from '@angular/material/button';
import { MainComponent } from './main/main.component';
import { RegisterComponent } from './register/register.component'
import { ReactiveFormsModule } from '@angular/forms';
import { ErrorInterceptor } from './utilities/ErrorInterceptor';
import { HeaderComponent } from './header/header.component';
import { JwtInterceptor } from './utilities/JwtInterceptor';

import { MatIconModule} from '@angular/material/icon';
import { SidebarComponent } from './sidebar/sidebar.component';
import { UnitTypeWindowComponent } from './unit-type-window/unit-type-window.component'

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    MainComponent,
    RegisterComponent,
    HeaderComponent,
    SidebarComponent,
    UnitTypeWindowComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    MatIconModule,

    FlexLayoutModule, MatFormFieldModule, MatInputModule, MatButtonModule, MatCardModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
