import {OnInit, Type} from '@angular/core';
import {MatDialog} from '@angular/material';
import {BehaviorSubject, Observable} from 'rxjs';
import {filter, repeatWhen} from 'rxjs/operators';
import {AbstractData} from '../services/abstract-data';
import {DetailComponent} from './detail/detail.component';

export abstract class AbstractOverview<T extends { id: string }> implements OnInit {
  private readonly refresh = new BehaviorSubject<null>(null);

  items$: Observable<T[]>;

  protected constructor(
    private readonly title: string,
    private readonly service: AbstractData<T>,
    private readonly matDialog: MatDialog,
    private readonly dialogComponent: Type<any> = DetailComponent,
  ) {
  }

  ngOnInit(): void {
    this.items$ = this.service.getAll().pipe(repeatWhen(() => this.refresh));
  }

  show(item: T): void {
    this.matDialog
      .open(this.dialogComponent, { data: { title: this.title, service: this.service, item } })
      .afterClosed().pipe(filter(refresh => refresh)).subscribe(() => this.refresh.next(null));
  }
}
