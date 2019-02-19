import {OnInit} from '@angular/core';
import {FormGroup} from '@angular/forms';
import {MatDialogRef} from '@angular/material';
import {Observable} from 'rxjs';

export abstract class AbstractDetail<T extends { id: string }> implements OnInit {
  abstract readonly form: FormGroup;

  protected constructor(
    private readonly saveFn: (item: T) => Observable<void>,
    protected readonly data: T,
    private readonly matDialogRef: MatDialogRef<any>,
  ) {
  }

  ngOnInit(): void {
    this.form.patchValue(this.data);
  }

  save(): void {
    this.form.disable();

    if (this.form.pristine) {
      this.matDialogRef.close();
      return;
    }

    this.saveFn({ ...this.form.value, id: this.data.id })
      .subscribe(() => this.matDialogRef.close(true), () => this.form.enable());
  }
}
