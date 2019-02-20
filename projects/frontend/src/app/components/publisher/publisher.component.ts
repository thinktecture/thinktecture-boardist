import {ChangeDetectionStrategy, Component, Inject} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {Publisher} from '../../models/publisher';
import {PublishersService} from '../../services/publishers.service';
import {AbstractDetail, DetailContext} from '../abstract-detail';

@Component({
  selector: 'ttb-publisher',
  templateUrl: './publisher.component.html',
  styleUrls: ['./publisher.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PublisherComponent extends AbstractDetail<PublishersService, Publisher> {
  readonly form = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(250)]],
    priority: [null, Validators.required],
    boardGameGeekId: [null, Validators.pattern(/\d*/)],
  });

  constructor(
    private readonly fb: FormBuilder,
    @Inject(MAT_DIALOG_DATA) context: DetailContext<PublishersService, Publisher>,
    matDialogRef: MatDialogRef<PublisherComponent>,
  ) {
    super(context, matDialogRef);
  }
}
