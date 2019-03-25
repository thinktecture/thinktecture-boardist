import { ChangeDetectionStrategy, Component } from '@angular/core';
import { MatDialog } from '@angular/material';
import { Publisher } from '../../models/publisher';
import { PublishersService } from '../../services/publishers.service';
import { AbstractOverview } from '../abstract-overview';
import { PublisherComponent } from '../publisher/publisher.component';

@Component({
  selector: 'ttb-publishers',
  templateUrl: './publishers.component.html',
  styleUrls: ['./publishers.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PublishersComponent extends AbstractOverview<PublishersService, Publisher> {
  constructor(publishers: PublishersService, matDialog: MatDialog) {
    super({ title: 'Publishers', service: publishers, detail: PublisherComponent, dialogConfig: { width: '700px' } }, matDialog);
  }
}
