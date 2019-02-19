import {ChangeDetectionStrategy, Component} from '@angular/core';
import {MatDialog} from '@angular/material';
import {Mechanic} from '../../models/mechanic';
import {MechanicsService} from '../../services/mechanics.service';
import {AbstractOverview} from '../abstract-overview';

@Component({
  selector: 'ttb-mechanics',
  templateUrl: './mechanics.component.html',
  styleUrls: ['./mechanics.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class MechanicsComponent extends AbstractOverview<Mechanic> {
  constructor(mechanics: MechanicsService, matDialog: MatDialog) {
    super('Mechanics', mechanics, matDialog);
  }
}
