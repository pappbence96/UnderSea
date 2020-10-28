import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { LoginResponseDto } from '../dtos/LoginResponseDto';
import { API_URL } from '../utilities/UrlInjectionToken';

@Injectable({ providedIn: 'root' })
export class AuthService {
    private currentLoginSubject: BehaviorSubject<LoginResponseDto>;
    public currentLogin: Observable<LoginResponseDto>;

    constructor(private http: HttpClient, @Inject(API_URL) private apiUrl: string) {
        this.currentLoginSubject = new BehaviorSubject<LoginResponseDto>(JSON.parse(localStorage.getItem('currentLogin')));
        this.currentLogin = this.currentLoginSubject.asObservable();
    }

    public get currentLoginValue(): LoginResponseDto {
        return this.currentLoginSubject.value;
    }

    register(username: string, password: string, confirmPassword: string, countryName: string): Observable<any> {
        return this.http.post<any>(
            this.apiUrl + '/identity/register',
            { username, password, confirmPassword, countryName }
        );
    }

    login(username: string, password: string): Observable<LoginResponseDto> {
        return this.http.post<LoginResponseDto>(
            this.apiUrl + '/identity/login',
            { username, password }
        ).pipe(map(login => {
            localStorage.setItem('currentLogin', JSON.stringify(login));
            this.currentLoginSubject.next(login);
            return login;
        }));
    }

    logout(): void {
        localStorage.removeItem('currentLogin');
        this.currentLoginSubject.next(null);
    }
}
