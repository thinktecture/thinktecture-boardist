import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import Dexie from 'dexie';
import { defer, from, merge, Observable, of, Subject, throwError, timer } from 'rxjs';
import { environment } from '../../environments/environment';
import {
  catchError,
  concatMap,
  filter,
  finalize,
  map,
  mapTo,
  repeatWhen,
  retry,
  switchMap,
  tap,
} from 'rxjs/operators';
import { Syncable } from '../models/syncable';

interface Timestamp {
  name: string;
  timestamp: string;
}

interface Sync {
  timestamp: string;
  changed: Syncable[];
  deleted: string[];
}

interface Force {
  name: string;
  done: Subject<void>;
}

@Injectable({
  providedIn: 'root',
})
export class SyncService extends Dexie {
  private readonly timestamps: Dexie.Table<Timestamp, string>;
  private readonly refreshed = new Subject<string>();
  private readonly force = new Subject<Force>();

  constructor(private readonly httpClient: HttpClient) {
    super('sync');

    this.version(1).stores({
      timestamps: '&name',
      categories: '&id',
      games: '&id',
      mechanics: '&id',
      persons: '&id',
      publishers: '&id',
    });
  }

  sync(name: string): void {
    let idle = true;

    timer(environment.syncStartDelay, environment.syncCheckInterval).pipe(
      filter(() => idle && navigator.onLine),
      tap(() => idle = false),
      switchMap(() => merge(
        of(new Subject<void>()),
        this.force.pipe(
          filter(force => force.name === name),
          map(force => force.done),
        ),
      )),
      switchMap(force => from(this.timestamps.where({ name }).first()).pipe(
        switchMap(status => this.httpClient.get<Sync>(`${environment.baseApiUrl}${name}/sync${status ? `/${status.timestamp}` : ''}`).pipe(
          catchError(err => timer(environment.syncStartDelay).pipe(
            switchMap(() => throwError(err)),
          )),
          retry(environment.syncRetry),
        )),
        concatMap(({ timestamp, changed, deleted }) => {
          const table = this.table(name);
          return from(Promise.all([table.bulkDelete(deleted), table.bulkPut(changed)])).pipe(
            switchMap(() => this.timestamps.put({ name, timestamp })),
          );
        }),
        tap(() => {
          this.refreshed.next(name);

          force.next();
          force.complete();
        }),
        finalize(() => idle = true),
      )),
      retry(),
    ).subscribe();
  }

  update(name: string): Observable<void> {
    const done = new Subject<void>();
    this.force.next({ name, done });
    return done;
  }

  private waitForRefresh(name: string): Observable<void> {
    return this.refreshed.pipe(
      filter(refresh => refresh === name),
      mapTo(undefined),
    );
  }

  getAll<T>(name: string): Observable<T[]> {
    return defer(() => this.table(name).toArray()).pipe(repeatWhen(() => this.waitForRefresh(name)));
  }

  get<T>(name: string, id: string): Observable<T> {
    return defer(() => this.table(name).where({ id }).first()).pipe(repeatWhen(() => this.waitForRefresh(name)));
  }

  async put<T>(name: string, item: T, options: { emitRefresh?: boolean } = {}): Promise<void> {
    await this.table(name).put(item);

    if (options.emitRefresh !== false) {
      this.refreshed.next(name);
    }
  }
}
