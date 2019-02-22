import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category } from '../models/category';
import { AbstractData } from './abstract-data';
import { SyncService } from './sync.service';

@Injectable({
  providedIn: 'root',
})
export class CategoriesService extends AbstractData<Category> {
  constructor(httpClient: HttpClient, sync: SyncService) {
    super(httpClient, sync, 'categories');
  }
}
