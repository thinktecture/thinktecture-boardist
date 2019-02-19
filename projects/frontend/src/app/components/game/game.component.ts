import {ChangeDetectionStrategy, Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {Observable} from 'rxjs';
import {map, tap} from 'rxjs/operators';
import {Game, GameDetail} from '../../models/game';
import {Person} from '../../models/person';
import {Publisher} from '../../models/publisher';
import {GamesService} from '../../services/games.service';
import {PersonsService} from '../../services/persons.service';
import {PublishersService} from '../../services/publishers.service';

@Component({
  selector: 'ttb-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GameComponent implements OnInit {
  game$: Observable<GameDetail>;
  games$: Observable<Game[]>;
  publishers$: Observable<Publisher[]>;
  persons$: Observable<Person[]>;

  readonly form = this.fb.group({
    name: ['', Validators.required],
    mainGameId: [null],
    minPlayers: ['', Validators.required],
    maxPlayers: ['', Validators.required],
    minDuration: [null],
    maxDuration: [null],
    buyDate: [null],
    buyPrice: [null],
    publisherId: ['', Validators.required],
    boardGameGeekId: [null, Validators.pattern(/\d*/)],
    authors: [[]],
    illustrators: [[]],
  });

  constructor(
    private readonly fb: FormBuilder,
    private readonly games: GamesService,
    private readonly publishers: PublishersService,
    private readonly persons: PersonsService,
    @Inject(MAT_DIALOG_DATA) public readonly id: string,
    private readonly matDialogRef: MatDialogRef<GameComponent>,
  ) {
  }

  ngOnInit(): void {
    this.game$ = this.games.get$(this.id).pipe(
      tap(game => this.form.patchValue(game)),
    );
    this.games$ = this.games.getAll$().pipe(
      map(games => games.filter(game => !game.mainGameId)),
    );
    this.publishers$ = this.publishers.getAll$();
    this.persons$ = this.persons.getAll$();
  }

  save(): void {
    if (this.form.pristine) {
      this.matDialogRef.close();
      return;
    }

    this.games.save$({ ...this.form.value, id: this.id })
      .subscribe(() => this.matDialogRef.close(true));
  }
}
