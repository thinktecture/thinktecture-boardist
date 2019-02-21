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
export class GamesComponent extends AbstractOverview<GamesService, Game> {
  readonly columns = ['name', 'publisher', 'minAge', 'minPlayers', 'maxPlayers', 'isExtension', 'boardGameGeekId'];

  constructor(games: GamesService, matDialog: MatDialog) {
    super({ title: 'Games', service: games, detail: GameComponent, dialogConfig: { width: '900px' } }, matDialog);
  }
}
