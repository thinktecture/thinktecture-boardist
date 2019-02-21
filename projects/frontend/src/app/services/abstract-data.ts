import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {map, shareReplay, tap} from 'rxjs/operators';
import {environment} from '../../environments/environment';
import {Item} from '../models/item';

export abstract class AbstractData<T extends Item> {
  private cache: Observable<T[]> | null = null;

  protected constructor(protected readonly httpClient: HttpClient, protected readonly endpoint: string) {
  }

  getAll(): Observable<T[]> {
    return this.cache || (this.cache = this.httpClient.get<T[]>(`${environment.baseApiUrl}${this.endpoint}`).pipe(shareReplay(1)));
  }

  get(id: string): Observable<T> {
    return this.getAll().pipe(
      map(items => items.find(item => item.id === id)),
    );
  }

  import(id: string, overwrite = false): Observable<T | null> {
    const params = { overwrite: overwrite.toString() };
    return this.httpClient.post<T | null>(`${environment.baseApiUrl}${this.endpoint}/${id}/import`, null, { params });
  }

  save(item: T): Observable<T> {
    return this.httpClient[item.id ? 'put' : 'post']<T | null>(`${environment.baseApiUrl}${this.endpoint}`, item).pipe(
      map(result => result || item),
      tap(() => this.clearCache()),
    );
  }

  clearCache(): void {
    this.cache = null;
  }
}
