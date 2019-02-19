import {OnInit, Type} from '@angular/core';
import {MatDialog} from '@angular/material';
import {BehaviorSubject, Observable} from 'rxjs';
import {filter, repeatWhen} from 'rxjs/operators';

export abstract class AbstractOverview<T> implements OnInit {
  private readonly refresh = new BehaviorSubject<null>(null);

  items$: Observable<T[]>;

  protected constructor(
    private readonly getAll: () => Observable<T[]>,
    private readonly matDialog: MatDialog,
    private readonly dialogComponent: Type<any>,
  ) {
  }

  ngOnInit(): void {
    this.items$ = this.getAll().pipe(repeatWhen(() => this.refresh));
  }

  show(item: T): void {
    this.matDialog
      .open(this.dialogComponent, { data: item })
      .afterClosed().pipe(filter(refresh => refresh)).subscribe(() => this.refresh.next(null));
  }
}
