import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';
import {Game} from '../models/game';
import {AbstractData} from './abstract-data';

@Injectable({
  providedIn: 'root',
})
export class GamesService extends AbstractData<Game> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'games');
  }

  getAll(expansions = true): Observable<Game[]> {
    return this.httpClient.get<Game[]>(`${environment.baseApiUrl}${this.endpoint}`, { params: { expansions: expansions.toString() } });
  }

  import(id: string): Observable<Game | null> {
    return this.httpClient.post<Game | null>(`${environment.baseApiUrl}${this.endpoint}/${id}/import`, null);
  }
}
