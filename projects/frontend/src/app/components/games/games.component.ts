import {ChangeDetectionStrategy, Component} from '@angular/core';
import {MatDialog} from '@angular/material';
import {Game} from '../../models/game';
import {GamesService} from '../../services/games.service';
import {AbstractOverview} from '../abstract-overview';
import {GameComponent} from '../game/game.component';

@Component({
  selector: 'ttb-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GamesComponent extends AbstractOverview<Game> {
  constructor(games: GamesService, matDialog: MatDialog) {
    super(() => games.getAll(), matDialog, GameComponent);
  }
}
