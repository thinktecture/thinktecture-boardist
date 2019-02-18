import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';
import {environment} from '../../environments/environment';
import {Person} from '../models/person';

@Injectable({
  providedIn: 'root',
})
export class PersonsService {
  constructor(private readonly httpClient: HttpClient) {
  }

  getAll$(): Observable<Person[]> {
    return this.httpClient.get<Person[]>(`${environment.baseApiUrl}persons`);
  }

  get$(id: string): Observable<Person> {
    return this.httpClient.get<Person>(`${environment.baseApiUrl}persons/${id}`);
  }
}
