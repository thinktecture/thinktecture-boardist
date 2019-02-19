import {ChangeDetectionStrategy, Component, Inject} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {AbstractData} from '../../services/abstract-data';
import {AbstractDetail} from '../abstract-detail';

export interface DetailComponentContainer<T extends { id: string }> {
  title: string;
  service: AbstractData<T>;
  item: T;
}

@Component({
  selector: 'ttb-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DetailComponent extends AbstractDetail<any> {
  readonly form = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(250)]],
    boardGameGeekId: [null, Validators.pattern(/\d*/)],
  });

  constructor(
    private readonly fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) data: DetailComponentContainer<any>,
    matDialogRef: MatDialogRef<DetailComponent>,
  ) {
    super(item => data.service.save(item), data.item, matDialogRef);
  }
}
