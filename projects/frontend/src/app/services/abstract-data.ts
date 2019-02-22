import { HttpClient } from '@angular/common/http';
import { defer, from, Observable, of } from 'rxjs';
import { map, mapTo, switchMap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Item } from '../models/item';
import { SyncService } from './sync.service';

export abstract class AbstractData<T extends Item> {
  protected constructor(protected readonly httpClient: HttpClient, protected readonly sync: SyncService, protected readonly endpoint: string) {
  }

  getAll(): Observable<T[]> {
    return this.sync.getAll(this.endpoint);
    ;
  }

  get(id: string): Observable<T> {
    return this.sync.get(this.endpoint, id);
  }

  import(id: string, overwrite = false): Observable<T | null> {
    const params = { overwrite: overwrite.toString() };
    return this.httpClient.post<T | null>(`${environment.baseApiUrl}${this.endpoint}/${id}/import`, null, { params }).pipe(
      switchMap(result => result ? from(this.sync.put(this.endpoint, result)).pipe(mapTo(result)) : of(result)),
    );
  }

  save(item: T): Observable<T> {
    return this.httpClient[item.id ? 'put' : 'post']<T | null>(`${environment.baseApiUrl}${this.endpoint}`, item).pipe(
      map(result => result || item),
      switchMap(result => from(this.sync.put(this.endpoint, result)).pipe(mapTo(result))),
    );
  }

  update(item: T): Observable<void> {
    return defer(() => this.sync.put(this.endpoint, item));
  }

  clearCache(): Observable<void> {
    return this.sync.update(this.endpoint);
  }
}
