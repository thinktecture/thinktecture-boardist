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

  getAll$(): Observable<Publisher[]> {
    return this.httpClient.get<Publisher[]>(`${environment.baseApiUrl}publishers`);
  }

  get$(id: string): Observable<Publisher> {
    return this.httpClient.get<Publisher>(`${environment.baseApiUrl}publishers/${id}`);
  }
}
