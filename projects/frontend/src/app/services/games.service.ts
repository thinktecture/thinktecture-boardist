import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {map} from 'rxjs/operators';
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
    return super.getAll().pipe(
      map(items => items.filter(item => expansions || !item.mainGameId)),
    );
  }

  import(id: string, overwrite: boolean): Observable<Game | null> {
    const params = { overwrite: overwrite.toString() };
    return this.httpClient.post<Game | null>(`${environment.baseApiUrl}${this.endpoint}/${id}/import`, null, { params });
  }

  search(query: string): Observable<number | null> {
    return this.httpClient.get<number | null>(`${environment.baseApiUrl}${this.endpoint}/lookup`, { params: { query } });
  }
}
