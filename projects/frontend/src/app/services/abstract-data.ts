import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Item } from '../models/item';
import { SyncService } from './sync.service';

export abstract class AbstractData<T extends Item> {
  protected constructor(protected readonly httpClient: HttpClient, protected readonly endpoint: string, protected readonly sync: SyncService) {
  }

  getAll(): Observable<T[]> {
    return this.sync.getAll(this.endpoint);
  }

  get(id: string): Observable<T> {
    return this.sync.get(this.endpoint, id);
  }

  async import(id: string, overwrite = false): Promise<T | null> {
    const params = { overwrite: overwrite.toString() };
    const result = await this.httpClient.post<T | null>(`${environment.baseApiUrl}${this.endpoint}/${id}/import`, null, { params }).toPromise();
    if (result) {
      await this.sync.put(this.endpoint, result);
    }

    return result;
  }

  async save(item: T): Promise<T> {
    const method = item.id ? 'put' : 'post';
    const result = await this.httpClient[method]<T | null>(`${environment.baseApiUrl}${this.endpoint}`, item).toPromise();

    await this.sync.put(this.endpoint, result || item);

    return result || item;
  }

  async update(item: T, options: { emitRefresh?: boolean } = {}): Promise<void> {
    await this.sync.put(this.endpoint, item, options);
  }

  async clearCache(): Promise<void> {
    await this.sync.update(this.endpoint);
  }
}
