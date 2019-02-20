import {ChangeDetectionStrategy, Component, Inject} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {AbstractDetail, DetailContext} from '../abstract-detail';

@Component({
  selector: 'ttb-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DetailComponent extends AbstractDetail<any, any> {
  readonly form = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(250)]],
    boardGameGeekId: [null, Validators.pattern(/\d*/)],
  });

  constructor(
    private readonly fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) context: DetailContext<any, any>,
    matDialogRef: MatDialogRef<DetailComponent>,
  ) {
    super(context, matDialogRef);
  }
}
