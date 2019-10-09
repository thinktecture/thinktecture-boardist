import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Publisher } from '../models/publisher';
import { AbstractData } from './abstract-data';
import { SyncService } from './sync.service';

@Injectable({
  providedIn: 'root',
})
export class PublishersService extends AbstractData<Publisher> {
  constructor(httpClient: HttpClient, sync: SyncService) {
    super(httpClient, 'publishers', sync);
  }
}
