import {OnInit} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {MatDialogRef} from '@angular/material';
import {Item} from '../models/item';
import {AbstractData} from '../services/abstract-data';

export interface DetailContext<S extends AbstractData<T>, T extends Item> {
  title: string;
  service: S;
  item: T;
}

export abstract class AbstractDetail<S extends AbstractData<T>, T extends Item> implements OnInit {
  abstract readonly form: FormGroup;

  protected constructor(protected readonly context: DetailContext<S, T>, private readonly matDialogRef: MatDialogRef<any>) {
  }

  ngOnInit(): void {
    this.form.patchValue(this.context.item);
  }

  save(): void {
    this.form.disable();

    if (this.form.pristine) {
      this.matDialogRef.close();
      return;
    }

    this.context.service.save({ ...this.form.value, id: this.context.item.id })
      .subscribe(() => this.matDialogRef.close(true), () => this.form.enable());
  }
}
