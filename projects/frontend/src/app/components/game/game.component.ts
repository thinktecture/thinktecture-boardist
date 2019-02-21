import {ChangeDetectionStrategy, Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {combineLatest, defer, Observable} from 'rxjs';
import {filter, finalize, map, repeatWhen} from 'rxjs/operators';
import {environment} from '../../../environments/environment';
import {Category} from '../../models/category';
import {Game} from '../../models/game';
import {Mechanic} from '../../models/mechanic';
import {Person} from '../../models/person';
import {Publisher} from '../../models/publisher';
import {CategoriesService} from '../../services/categories.service';
import {GamesService} from '../../services/games.service';
import {MechanicsService} from '../../services/mechanics.service';
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
  searching = false;

  data$: Observable<{
    games: Game[],
    publishers: Publisher[],
    persons: Person[],
    categories: Category[],
    mechanics: Mechanic[],
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
    publisherId: [null],
    boardGameGeekId: [null, Validators.pattern(/\d*/)],
    authors: [[]],
    illustrators: [[]],
    categories: [[]],
    mechanics: [[]],
  });

  get coverSrc(): string {
    return this.context.item.id ? `url(${environment.baseApiUrl}binaries/${this.context.item.id}/logo)` : '';
  }

  constructor(
    private readonly fb: FormBuilder,
    private readonly publishers: PublishersService,
    private readonly persons: PersonsService,
    private readonly categories: CategoriesService,
    private readonly mechanics: MechanicsService,
    @Inject(MAT_DIALOG_DATA) context: DetailContext<GamesService, Game>,
    matDialogRef: MatDialogRef<GameComponent>,
  ) {
    super(context, matDialogRef);
  }

  ngOnInit(): void {
    super.ngOnInit();

    this.data$ = defer(() => combineLatest(
      this.context.service.getAll(false),
      this.publishers.getAll(),
      this.persons.getAll(),
      this.categories.getAll(),
      this.mechanics.getAll(),
    )).pipe(
      map(([games, publishers, persons, categories, mechanics]) => ({ games, publishers, persons, categories, mechanics })),
      repeatWhen(() => this.refresh),
    );
  }

  protected afterImport(): void {
    super.afterImport();

    this.publishers.clearCache();
    this.persons.clearCache();
    this.categories.clearCache();
    this.mechanics.clearCache();
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
