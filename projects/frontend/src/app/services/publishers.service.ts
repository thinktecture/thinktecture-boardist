import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';
import {Publisher} from '../models/publisher';

@Injectable({
  providedIn: 'root',
})
export class PublishersService {
  constructor(private readonly httpClient: HttpClient) {
  }

  getAll(): Observable<Publisher[]> {
    return this.httpClient.get<Publisher[]>(`${environment.baseApiUrl}publishers`);
  }

  save(publisher: Publisher): Observable<void> {
    return this.httpClient[publisher.id ? 'put' : 'post']<void>(`${environment.baseApiUrl}publishers`, publisher);
  }
}
