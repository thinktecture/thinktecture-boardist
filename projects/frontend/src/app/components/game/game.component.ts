import {ChangeDetectionStrategy, Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {combineLatest, iif, Observable, of, Subject} from 'rxjs';
import {filter, finalize, map, repeatWhen, switchMap} from 'rxjs/operators';
import {Game} from '../../models/game';
import {Person} from '../../models/person';
import {Publisher} from '../../models/publisher';
import {GamesService} from '../../services/games.service';
import {PersonsService} from '../../services/persons.service';
import {PublishersService} from '../../services/publishers.service';
import {AbstractDetail, DetailContext} from '../abstract-detail';

@Component({
  selector: 'ttb-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class GameComponent extends AbstractDetail<GamesService, Game> implements OnInit {
  private readonly refresh = new Subject<void>();

  searching = false;
  importing = false;

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
    private readonly publishers: PublishersService,
    private readonly persons: PersonsService,
    @Inject(MAT_DIALOG_DATA) context: DetailContext<GamesService, Game>,
    matDialogRef: MatDialogRef<GameComponent>,
  ) {
    super(context, matDialogRef);
  }

  ngOnInit(): void {
    super.ngOnInit();

    this.data$ = combineLatest(
      this.context.service.getAll(false),
      this.publishers.getAll(),
      this.persons.getAll(),
    ).pipe(
      map(([games, publishers, persons]) => ({ games, publishers, persons })),
      repeatWhen(() => this.refresh),
    );
  }

  import(): void {
    this.form.disable();

    this.importing = true;

    iif(() => this.form.dirty, this.save(false), of(false)).pipe(
      switchMap(() => this.context.service.import(this.context.item.id)),
      filter(result => result !== null),
      finalize(() => {
        this.form.enable();
        this.importing = false;
      }),
    ).subscribe(result => {
      this.form.patchValue(result);
      this.form.markAsPristine();
      this.form.markAsUntouched();

      this.reload = true;
      this.refresh.next(null);
    });
  }

  search(): void {
    const { boardGameGeekId } = this.form.controls;
    boardGameGeekId.disable();

    this.searching = true;

    this.context.service.search(this.form.value.name).pipe(
      filter(result => result !== null),
      finalize(() => {
        boardGameGeekId.enable();
        this.searching = false;
      }),
    ).subscribe(result => {
      this.form.patchValue({ boardGameGeekId: result });
      boardGameGeekId.markAsDirty();
      boardGameGeekId.markAsTouched();
    });
  }
}
