import {ChangeDetectionStrategy, Component} from '@angular/core';
import {MatDialog} from '@angular/material';
import {Publisher} from '../../models/publisher';
import {PublishersService} from '../../services/publishers.service';
import {AbstractOverview} from '../abstract-overview';

@Component({
  selector: 'ttb-publishers',
  templateUrl: './publishers.component.html',
  styleUrls: ['./publishers.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PublishersComponent extends AbstractOverview<Publisher> {
  constructor(publishers: PublishersService, matDialog: MatDialog) {
    super('Publishers', publishers, matDialog);
  }
}
