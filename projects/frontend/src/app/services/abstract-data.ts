import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { shareReplay } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Item } from '../models/item';

export abstract class AbstractData<T extends Item> {
  private cache: Observable<T[]> | null = null;

  protected constructor(protected readonly httpClient: HttpClient, protected readonly endpoint: string) {
  }

  getAll(): Promise<T[]> {
    if (!this.cache) {
      this.cache = this.httpClient.get<T[]>(`${environment.baseApiUrl}${this.endpoint}`).pipe(shareReplay(1));
    }

    return this.cache.toPromise();
  }

  async get(id: string): Promise<T> {
    const items = await this.getAll();
    return items.find(item => item.id === id);
  }

  import(id: string, overwrite = false): Promise<T | null> {
    const params = { overwrite: overwrite.toString() };
    return this.httpClient.post<T | null>(`${environment.baseApiUrl}${this.endpoint}/${id}/import`, null, { params }).toPromise();
  }

  async save(item: T): Promise<T> {
    const method = item.id ? 'put' : 'post';
    const result = await this.httpClient[method]<T | null>(`${environment.baseApiUrl}${this.endpoint}`, item).toPromise();
    await this.clearCache();
    return result || item;
  }

  async clearCache(): Promise<void> {
    this.cache = null;
  }
}
