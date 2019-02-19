import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';
import {Game} from '../models/game';

@Injectable({
  providedIn: 'root',
})
export class GamesService {
  constructor(private readonly httpClient: HttpClient) {
  }

  getAll(expansions = true): Observable<Game[]> {
    return this.httpClient.get<Game[]>(`${environment.baseApiUrl}games`, { params: { expansions: expansions.toString() } });
  }

  save(game: Game): Observable<void> {
    return this.httpClient[game.id ? 'put' : 'post']<void>(`${environment.baseApiUrl}games`, game);
  }

  import(id: string): Observable<Game | null> {
    return this.httpClient.post<Game | null>(`${environment.baseApiUrl}games/${id}/import`, null);
  }
}
