import {ChangeDetectionStrategy, Component, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material';
import {BehaviorSubject, Observable} from 'rxjs';
import {filter, switchMap} from 'rxjs/operators';
import {Publisher} from '../../models/publisher';
import {PublishersService} from '../../services/publishers.service';
import {PublisherComponent} from '../publisher/publisher.component';

@Component({
  selector: 'ttb-publishers',
  templateUrl: './publishers.component.html',
  styleUrls: ['./publishers.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PublishersComponent implements OnInit {
  private readonly refresh = new BehaviorSubject<null>(null);

  publishers$: Observable<Publisher[]>;

  constructor(private readonly publishers: PublishersService, private readonly matDialog: MatDialog) {
  }

  ngOnInit() {
    this.publishers$ = this.refresh.pipe(
      switchMap(() => this.publishers.getAll$()),
    );
  }

  show(id: string): void {
    this.matDialog.open(PublisherComponent, { data: id }).afterClosed().pipe(
      filter(refresh => refresh),
    ).subscribe(() => this.refresh.next(null));
  }
}
