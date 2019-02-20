import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';
import {Item} from '../models/item';

export abstract class AbstractData<T extends Item> {
  protected constructor(protected readonly httpClient: HttpClient, protected readonly endpoint: string) {
  }

  getAll(): Observable<T[]> {
    return this.httpClient.get<T[]>(`${environment.baseApiUrl}${this.endpoint}`);
  }

  save(item: T): Observable<void> {
    return this.httpClient[item.id ? 'put' : 'post']<void>(`${environment.baseApiUrl}${this.endpoint}`, item);
  }
}
