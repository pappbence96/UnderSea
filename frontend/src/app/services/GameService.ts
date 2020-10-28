import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GameDataDto } from '../dtos/GameDataDto';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';
import { API_URL } from '../utilities/UrlInjectionToken';


@Injectable({ providedIn: 'root' })
export class GameService {
    private gameDataSubject: BehaviorSubject<GameDataDto>;
    public gameData: Observable<GameDataDto>;

    constructor(private http: HttpClient, @Inject(API_URL) private apiUrl: string) {
        this.gameDataSubject = new BehaviorSubject<GameDataDto>(new GameDataDto());
        this.gameData = this.gameDataSubject.asObservable();
    }

    loadGameData(): Observable<GameDataDto> {
        return this.http
            .get<GameDataDto>(this.apiUrl + '/main')
            .pipe(map(data => {
                this.gameDataSubject.next(data);
                return data;
            }));
    }
}
