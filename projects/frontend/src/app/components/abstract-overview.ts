import {InjectionToken, OnDestroy, OnInit, Type} from '@angular/core';
import {MatDialog, Sort} from '@angular/material';
import {ActivatedRoute, Router} from '@angular/router';
import {combineLatest, defer, Observable, Subject, Subscription} from 'rxjs';
import {distinctUntilChanged, filter, map, repeatWhen} from 'rxjs/operators';
import {Item} from '../models/item';
import {AbstractData} from '../services/abstract-data';
import {DetailContext} from './abstract-detail';

function compare(a: number | string, b: number | string, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}

export interface OverviewContext<S extends AbstractData<T>, T extends Item> {
  title: string;
  service: S;
  detail: Type<any>;
}

export const OVERVIEW_CONTEXT = new InjectionToken<OverviewContext<any, any>>('overview context');

export abstract class AbstractOverview<S extends AbstractData<T>, T extends Item> implements OnInit, OnDestroy {
  private readonly refresh = new Subject<void>();

  private subscription = Subscription.EMPTY;

  items$: Observable<T[]>;
  sort: Sort = { active: 'name', direction: 'asc' };

  protected constructor(
    private readonly context: OverviewContext<S, T>,
    private readonly matDialog: MatDialog,
    private readonly route: ActivatedRoute,
    private readonly router: Router,
  ) {
  }

  ngOnInit(): void {
    this.items$ = defer(() => this.context.service.getAll()).pipe(
      map(items => this.sortData(items)),
      repeatWhen(() => this.refresh),
    );

    this.subscription = combineLatest(
      this.items$,
      this.route.queryParams.pipe(
        map(params => params.id),
        distinctUntilChanged(),
      ),
    ).pipe(
      filter(([items, id]) => id),
      map(([items, id]) => items.find(item => item.id === id)),
    ).subscribe(item => this.show(item));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  private sortData(items: T[]): T[] {
    return !this.sort || !this.sort.active || this.sort.direction === ''
      ? items
      : items.slice().sort((a, b) => compare(a[this.sort.active], b[this.sort.active], this.sort.direction === 'asc'));
  }

  resort(sort: Sort): void {
    this.sort = sort;
    this.refresh.next();
  }

  add(): void {
    this.show({} as T);
  }

  show(item: T): void {
    this.matDialog
      .open<any, DetailContext<S, T>, boolean>(this.context.detail, { data: { ...this.context, item } })
      .afterClosed()
      .subscribe(async refresh => {
        await this.router.navigate([], { queryParams: {} });

        if (refresh) {
          this.refresh.next(null);
        }
      });
  }
}
