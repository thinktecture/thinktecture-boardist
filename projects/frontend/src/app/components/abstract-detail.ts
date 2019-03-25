import { OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { iif, Observable, of, Subject } from 'rxjs';
import { filter, finalize, switchMap, tap } from 'rxjs/operators';
import { Item } from '../models/item';
import { AbstractData } from '../services/abstract-data';

export interface DetailContext<S extends AbstractData<T>, T extends Item> {
  title: string;
  service: S;
  item: T;
}

export abstract class AbstractDetail<S extends AbstractData<T>, T extends Item> implements OnInit {
  protected readonly refresh = new Subject<void>();

  abstract readonly form: FormGroup;

  reload = false;
  importing = false;

  protected constructor(readonly context: DetailContext<S, T>, protected readonly matDialogRef: MatDialogRef<any>) {
  }

  ngOnInit(): void {
    this.form.patchValue(this.context.item);
  }

  saveAndClose(): void {
    this.form.disable();

    if (this.form.pristine) {
      this.matDialogRef.close();
      return;
    }

    this.reload = true;

    this.save(true).pipe(
      finalize(() => () => this.form.enable()),
    ).subscribe();
  }

  importFromBoardGameGeek(): void {
    this.form.disable();

    this.importing = true;

    iif(() => this.form.dirty, this.save(false), of(this.context.item)).pipe(
      switchMap(item => this.context.service.import(item.id, this.form.controls.boardGameGeekId.dirty)),
      filter(result => result !== null),
      tap(() => this.afterImport()),
      finalize(() => {
        this.form.enable();
        this.importing = false;
      }),
    ).subscribe(result => {
      this.form.patchValue(result);
      this.form.markAsPristine();
      this.form.markAsUntouched();

      this.reload = true;
      this.refresh.next(null);
    });
  }

  protected afterImport(): void {
    this.context.service.clearCache();
  }

  protected save(close = true): Observable<T> {
    return this.context.service.save({ ...this.form.value, id: this.context.item.id }).pipe(
      tap(() => close && this.matDialogRef.close(this.reload)),
    );
  }
}
