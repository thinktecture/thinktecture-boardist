import {ChangeDetectionStrategy, Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {BehaviorSubject, combineLatest, Observable} from 'rxjs';
import {filter, finalize, map, repeatWhen, tap} from 'rxjs/operators';
import {Game} from '../../models/game';
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
  private readonly refresh = new BehaviorSubject<null>(null);

  data$: Observable<{
    games: Game[],
    publishers: Publisher[],
    persons: Person[],
  }>;

  readonly form = this.fb.group({
    name: ['', [Validators.required, Validators.maxLength(250)]],
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
    @Inject(MAT_DIALOG_DATA) public readonly game: Game,
    private readonly matDialogRef: MatDialogRef<GameComponent>,
  ) {
  }

  ngOnInit(): void {
    this.form.patchValue(this.game);

    this.data$ = combineLatest(
      this.games.getAll(false),
      this.publishers.getAll(),
      this.persons.getAll$(),
    ).pipe(
      map(([games, publishers, persons]) => ({ games, publishers, persons })),
      repeatWhen(() => this.refresh),
    );
  }

  save(): void {
    this.form.disable();

    if (this.form.pristine) {
      this.matDialogRef.close();
      return;
    }

    this.games.save({ ...this.form.value, id: this.game.id })
      .subscribe(() => this.matDialogRef.close(true), () => this.form.enable());
  }

  import(): void {
    this.form.disable();

    this.games.import(this.game.id).pipe(
      filter(result => result !== null),
      tap(result => this.form.patchValue(result)),
      finalize(() => this.form.enable()),
    ).subscribe(() => this.refresh.next(null));
  }
}
