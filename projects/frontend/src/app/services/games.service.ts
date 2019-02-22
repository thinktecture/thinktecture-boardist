import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Game } from '../models/game';
import { AbstractData } from './abstract-data';
import { SyncService } from './sync.service';

export enum FileCategory {
  Logo = 'logo',
  Rules = 'rules',
}

@Injectable({
  providedIn: 'root',
})
export class GamesService extends AbstractData<Game> {
  constructor(httpClient: HttpClient, sync: SyncService) {
    super(httpClient, sync, 'games');
  }

  getAll(expansions = true): Observable<Game[]> {
    return super.getAll().pipe(
      map(items => items.filter(item => expansions || !item.mainGameId)),
    );
  }

  search(query: string): Observable<number | null> {
    return this.httpClient.get<number | null>(`${environment.baseApiUrl}${this.endpoint}/lookup`, { params: { query } });
  }

  upload(category: FileCategory, id: string, file: File): Observable<void> {
    const data = new FormData();
    data.append('id', id);
    data.append('file', file);

    return this.httpClient.post<void>(`${environment.baseApiUrl}binaries/${category}`, data);
  }

  getLogoUrl(id: string): string {
    return `${environment.baseApiUrl}binaries/${id}/${FileCategory.Logo}`;
  }

  getRulesUrl(id: string): string {
    return `${environment.baseApiUrl}binaries/${id}/${FileCategory.Rules}`;
  }
}
