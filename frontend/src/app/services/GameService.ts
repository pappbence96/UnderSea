import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { GameDataDto } from '../dtos/GameDataDto';
import { map } from 'rxjs/operators';
import { BehaviorSubject, Observable } from 'rxjs';


@Injectable({ providedIn: 'root' })
export class GameService {
    private gameDataSubject: BehaviorSubject<GameDataDto>;
    public gameData: Observable<GameDataDto>;

    constructor(private http: HttpClient) {
        this.gameDataSubject = new BehaviorSubject<GameDataDto>(new GameDataDto());
        this.gameData = this.gameDataSubject.asObservable();
    }

    loadGameData() {
        return this.http
            .get<GameDataDto>("http://localhost:51554/api/main")
            .pipe(map(data => {
                this.gameDataSubject.next(data);
                return data;
            }));
    }
}