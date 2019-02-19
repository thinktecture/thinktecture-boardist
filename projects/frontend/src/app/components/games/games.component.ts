import {ChangeDetectionStrategy, Component, OnDestroy, OnInit} from '@angular/core';
import {MatDialog} from '@angular/material';
import {BehaviorSubject, Observable, Subscription} from 'rxjs';
import {filter, switchMap} from 'rxjs/operators';
import {Game} from '../../models/game';
import {GamesService} from '../../services/games.service';
import {GameComponent} from '../game/game.component';

@Component({
  selector: 'ttb-games',
  templateUrl: './games.component.html',
  styleUrls: ['./games.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GamesComponent implements OnInit, OnDestroy {
  private subscription = Subscription.EMPTY;
  private readonly refresh = new BehaviorSubject<null>(null);

  games$: Observable<Game[]>;

  constructor(private readonly games: GamesService, private readonly matDialog: MatDialog) {
  }

  ngOnInit(): void {
    this.games$ = this.refresh.pipe(
      switchMap(() => this.games.getAll$()),
    );
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  showGame(id: string): void {
    this.subscription = this.matDialog.open(GameComponent, {
      data: id,
      closeOnNavigation: true,
      disableClose: true,
      width: '500px',
      maxWidth: '90vw',
    }).afterClosed().pipe(
      filter(refresh => refresh),
    ).subscribe(() => this.refresh.next(null));
  }
}
