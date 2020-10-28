import { Injectable, Inject } from '@angular/core';
import { UnitTypeDto } from '../dtos/UnitTypeDto';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { API_URL } from '../utilities/UrlInjectionToken';

@Injectable({ providedIn: 'root' })
export class UnitsService {
    private typesSubject: BehaviorSubject<UnitTypeDto[]>;
    public types: Observable<UnitTypeDto[]>;

    constructor(private http: HttpClient, @Inject(API_URL) private apiUrl: string) {
        this.typesSubject = new BehaviorSubject<UnitTypeDto[]>([]);
        this.types = this.typesSubject.asObservable();
    }

    loadUnitTypes(): Observable<UnitTypeDto[]> {
        return this.http
            .get<UnitTypeDto[]>(this.apiUrl + '/units/types')
            .pipe(map(data => {
                this.typesSubject.next(data);
                return data;
            }));
    }
}
