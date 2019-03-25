import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Person } from '../models/person';
import { AbstractData } from './abstract-data';

@Injectable({
  providedIn: 'root',
})
export class PersonsService extends AbstractData<Person> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'persons');
  }
}
