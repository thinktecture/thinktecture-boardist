import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Game } from '../models/game';
import { AbstractData } from './abstract-data';

export enum FileCategory {
  Logo = 'logo',
  Rules = 'rules',
}

@Injectable({
  providedIn: 'root',
})
export class GamesService extends AbstractData<Game> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'games');
  }

  async getAll(expansions = true): Promise<Game[]> {
    const items = await super.getAll();
    return items.filter(item => expansions || !item.mainGameId);
  }

  search(query: string): Promise<number | null> {
    return this.httpClient.get<number | null>(`${environment.baseApiUrl}${this.endpoint}/lookup`, { params: { query } }).toPromise();
  }

  async upload(category: FileCategory, id: string, file: File): Promise<void> {
    const data = new FormData();
    data.append('id', id);
    data.append('file', file);

    await this.httpClient.post<void>(`${environment.baseApiUrl}binaries/${category}`, data).toPromise();
  }

  getLogoUrl(id: string): string {
    return `${environment.baseApiUrl}binaries/${id}/${FileCategory.Logo}`;
  }

  getRulesUrl(id: string): string {
    return `${environment.baseApiUrl}binaries/${id}/${FileCategory.Rules}`;
  }
}
