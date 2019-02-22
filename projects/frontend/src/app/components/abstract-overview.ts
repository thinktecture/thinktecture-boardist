import { InjectionToken, OnInit, Type } from '@angular/core';
import { MatDialog, MatDialogConfig, Sort } from '@angular/material';
import { defer, Observable, Subject } from 'rxjs';
import { filter, map, repeatWhen } from 'rxjs/operators';
import { Item } from '../models/item';
import { AbstractData } from '../services/abstract-data';
import { DetailContext } from './abstract-detail';

function compare(a: number | string, b: number | string, isAsc: boolean) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}

export interface OverviewContext<S extends AbstractData<T>, T extends Item> {
  title: string;
  service: S;
  detail: Type<any>;
  dialogConfig?: MatDialogConfig<DetailContext<S, T>>,
}

export const OVERVIEW_CONTEXT = new InjectionToken<OverviewContext<any, any>>('overview context');

export abstract class AbstractOverview<S extends AbstractData<T>, T extends Item> implements OnInit {
  private readonly refresh = new Subject<void>();

  items$: Observable<T[]>;
  sort: Sort = { active: 'name', direction: 'asc' };

  protected constructor(
    private readonly context: OverviewContext<S, T>,
    private readonly matDialog: MatDialog,
  ) {
  }

  ngOnInit(): void {
    this.items$ = defer(() => this.context.service.getAll()).pipe(
      map(items => this.sortData(items)),
      repeatWhen(() => this.refresh),
    );
  }

  private sortData(items: T[]): T[] {
    return !this.sort || !this.sort.active || this.sort.direction === ''
      ? items
      : items.slice().sort((a, b) => compare(a[this.sort.active], b[this.sort.active], this.sort.direction === 'asc'));
  }

  trackBy(index: number, item: T) {
    return item.id;
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
      .open<any, DetailContext<S, T>, boolean>(this.context.detail, { ...this.context.dialogConfig, data: { ...this.context, item } })
      .afterClosed().pipe(
      filter(refresh => refresh),
    ).subscribe(() => this.refresh.next(null));
  }
}
