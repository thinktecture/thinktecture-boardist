import { OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialogRef } from '@angular/material';
import { Subject } from 'rxjs';
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

  async saveAndClose(): Promise<void> {
    this.form.disable();

    if (this.form.pristine) {
      this.matDialogRef.close();
      return;
    }

    this.reload = true;

    try {
      await this.save(true);
    } finally {
      this.form.enable();
    }
  }

  async importFromBoardGameGeek(): Promise<void> {
    this.form.disable();

    this.importing = true;

    try {
      let item = this.context.item;
      if (this.form.dirty) {
        item = await this.save(false);
      }

      const result = await this.context.service.import(item.id, this.form.controls.boardGameGeekId.dirty);
      if (result === null) {
        return;
      }

      await this.afterImport();

      this.form.patchValue(result);
      this.form.markAsPristine();
      this.form.markAsUntouched();

      this.reload = true;
      this.refresh.next();
    } finally {
      this.form.enable();
      this.importing = false;
    }
  }

  protected async afterImport(): Promise<void> {
    await this.context.service.clearCache();
  }

  protected async save(close: boolean): Promise<T> {
    const result = await this.context.service.save({ ...this.form.value, id: this.context.item.id });

    if (close) {
      this.matDialogRef.close(this.reload);
    }

    return result;
  }
}
