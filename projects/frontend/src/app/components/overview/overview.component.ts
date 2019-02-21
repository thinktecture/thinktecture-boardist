import {ChangeDetectionStrategy, Component, Inject} from '@angular/core';
import {MatDialog} from '@angular/material';
import {Item} from '../../models/item';
import {AbstractData} from '../../services/abstract-data';
import {AbstractOverview, OVERVIEW_CONTEXT, OverviewContext} from '../abstract-overview';

@Component({
  selector: 'ttb-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class OverviewComponent<S extends AbstractData<T>, T extends Item> extends AbstractOverview<S, T> {
  constructor(@Inject(OVERVIEW_CONTEXT) context: OverviewContext<any, any>, matDialog: MatDialog) {
    super(context, matDialog);
  }
}
