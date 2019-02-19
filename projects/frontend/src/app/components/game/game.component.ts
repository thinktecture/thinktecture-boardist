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
import {AbstractDetail} from '../abstract-detail';
import {DetailComponentContainer} from '../detail/detail.component';

@Component({
  selector: 'ttb-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GameComponent extends AbstractDetail<Game> implements OnInit {
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
    @Inject(MAT_DIALOG_DATA) data: DetailComponentContainer<Game>,
    matDialogRef: MatDialogRef<GameComponent>,
  ) {
    super(game => games.save(game), data.item, matDialogRef);
  }

  ngOnInit(): void {
    super.ngOnInit();

    this.data$ = combineLatest(
      this.games.getAll(false),
      this.publishers.getAll(),
      this.persons.getAll(),
    ).pipe(
      map(([games, publishers, persons]) => ({ games, publishers, persons })),
      repeatWhen(() => this.refresh),
    );
  }

  import(): void {
    this.form.disable();

    this.games.import(this.data.id).pipe(
      filter(result => result !== null),
      tap(result => this.form.patchValue(result)),
      finalize(() => this.form.enable()),
    ).subscribe(() => this.refresh.next(null));
  }
}
