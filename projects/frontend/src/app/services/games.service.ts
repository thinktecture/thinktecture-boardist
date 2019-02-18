import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import * as moment from 'moment';
import {Observable} from 'rxjs';
import {tap} from 'rxjs/operators';
import {environment} from '../../environments/environment';
import {Game, GameDetail} from '../models/game';

@Injectable({
  providedIn: 'root',
})
export class GamesService {
  constructor(private readonly httpClient: HttpClient) {
  }

  getAll$(): Observable<Game[]> {
    return this.httpClient.get<Game[]>(`${environment.baseApiUrl}games`);
  }

  get$(id: string): Observable<GameDetail> {
    return this.httpClient.get<GameDetail>(`${environment.baseApiUrl}games/${id}`).pipe(
      tap(game => game.buyDate = game.buyDate && moment(game.buyDate)),
    );
  }
}
