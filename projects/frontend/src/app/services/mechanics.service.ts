import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Mechanic } from '../models/mechanic';
import { AbstractData } from './abstract-data';

@Injectable({
  providedIn: 'root',
})
export class MechanicsService extends AbstractData<Mechanic> {
  constructor(httpClient: HttpClient) {
    super(httpClient, 'mechanics');
  }
}
