import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { LoginResponseDto } from "../dtos/LoginResponseDto";

@Injectable({ providedIn: 'root' })
export class AuthService {
    private currentLoginSubject: BehaviorSubject<LoginResponseDto>;
    public currentLogin: Observable<LoginResponseDto>;

    constructor(private http: HttpClient) {
        this.currentLoginSubject = new BehaviorSubject<LoginResponseDto>(JSON.parse(localStorage.getItem('currentLogin')));
        this.currentLogin = this.currentLoginSubject.asObservable();
    }

    public get currentLoginValue(): LoginResponseDto {
        return this.currentLoginSubject.value;
    }

    register(username: string, password: string, confirmPassword: string, countryName: string) {
        return this.http.post<any>(
            "http://localhost:51554/api/identity/register", 
            { username, password, confirmPassword, countryName}
        );
    }

    login(username: string, password: string) {
        return this.http.post<LoginResponseDto>(
            "http://localhost:51554/api/identity/login", 
            { username, password }
        ).pipe(map(login => {
            localStorage.setItem('currentLogin', JSON.stringify(login));
            this.currentLoginSubject.next(login); 
            return login;
        }));
    }

    logout() {
        localStorage.removeItem('currentLogin');
        this.currentLoginSubject.next(null);
    }
}