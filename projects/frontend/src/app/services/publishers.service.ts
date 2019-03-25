import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Publisher } from '../models/publisher';
import { AbstractData } from './abstract-data';

@Injectable({
  providedIn: 'root',
})
export class PublishersService extends AbstractData<Publisher> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'publishers');
  }
}
