import {ChangeDetectionStrategy, Component, Inject, OnInit} from '@angular/core';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA} from '@angular/material';
import {Observable} from 'rxjs';
import {tap} from 'rxjs/operators';
import {GameDetail} from '../../models/game';
import {GamesService} from '../../services/games.service';

@Component({
  selector: 'ttb-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GameComponent implements OnInit {
  game$: Observable<GameDetail>;

  readonly form = new FormGroup({
    name: new FormControl('', Validators.required),
    minPlayers: new FormControl('', Validators.required),
    maxPlayers: new FormControl('', Validators.required),
    minDuration: new FormControl(''),
    maxDuration: new FormControl(''),
    perPlayerDuration: new FormControl(''),
    buyDate: new FormControl(''),
    buyPrice: new FormControl(''),
  });

  constructor(private readonly games: GamesService, @Inject(MAT_DIALOG_DATA) public readonly id: string) {
  }

  ngOnInit(): void {
    this.game$ = this.games.get$(this.id).pipe(
      tap(game => this.form.patchValue(game)),
    );
  }
}
