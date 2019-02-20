import {OnInit} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {MatDialogRef} from '@angular/material';
import {Observable} from 'rxjs';
import {filter, finalize, tap} from 'rxjs/operators';
import {Item} from '../models/item';
import {AbstractData} from '../services/abstract-data';

export interface DetailContext<S extends AbstractData<T>, T extends Item> {
  title: string;
  service: S;
  item: T;
}

export abstract class AbstractDetail<S extends AbstractData<T>, T extends Item> implements OnInit {
  abstract readonly form: FormGroup;

  protected reload = false;

  protected constructor(protected readonly context: DetailContext<S, T>, private readonly matDialogRef: MatDialogRef<any>) {
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

  protected save(close = true): Observable<void> {
    return this.context.service.save({ ...this.form.value, id: this.context.item.id }).pipe(
      tap(() => close && this.matDialogRef.close(this.reload)),
    );
  }
}
