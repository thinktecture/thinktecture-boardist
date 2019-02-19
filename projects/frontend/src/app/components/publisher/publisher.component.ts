import {ChangeDetectionStrategy, Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {Publisher} from '../../models/publisher';
import {PublishersService} from '../../services/publishers.service';

@Component({
  selector: 'ttb-publisher',
  templateUrl: './publisher.component.html',
  styleUrls: ['./publisher.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PublisherComponent implements OnInit {
  readonly form = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(250)]],
  });

  constructor(
    private readonly fb: FormBuilder,
    private readonly publishers: PublishersService,
    @Inject(MAT_DIALOG_DATA) public readonly publisher: Publisher,
    private readonly matDialogRef: MatDialogRef<PublisherComponent>,
  ) {
  }

  ngOnInit(): void {
    this.form.patchValue(this.publisher);
  }

  save(): void {
    this.form.disable();

    if (this.form.pristine) {
      this.matDialogRef.close();
      return;
    }

    this.publishers.save({ ...this.form.value, id: this.publisher.id })
      .subscribe(() => this.matDialogRef.close(true), () => this.form.enable());
  }

  import(): void {
    this.form.disable();

    // TODO
  }
}
