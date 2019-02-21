import {ChangeDetectionStrategy, Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material';
import {combineLatest, defer, iif, Observable, of} from 'rxjs';
import {filter, finalize, map, mapTo, repeatWhen, switchMap, tap} from 'rxjs/operators';
import {environment} from '../../../environments/environment';
import {Category} from '../../models/category';
import {Game} from '../../models/game';
import {Mechanic} from '../../models/mechanic';
import {Person} from '../../models/person';
import {Publisher} from '../../models/publisher';
import {CategoriesService} from '../../services/categories.service';
import {FileCategory, GamesService} from '../../services/games.service';
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
    rules: [null],
  });

  get coverSrc(): string {
    return this.context.item.id ? `url(${environment.baseApiUrl}binaries/${this.context.item.id}/${FileCategory.Logo})` : '';
  }

  get rulesSrc(): string {
    return `${environment.baseApiUrl}binaries/${this.context.item.id}/${FileCategory.Rules}`;
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

  protected save(close = true): Observable<Game> {
    const { service } = this.context;
    return service.save({ ...this.form.value, id: this.context.item.id, rules: null }).pipe(
      switchMap(result =>
        iif(() => this.form.value.rules,
          service.upload(FileCategory.Rules, result.id, this.form.value.rules).pipe(
            tap(() => {
              this.context.item.hasRules = true;
              this.form.patchValue({rules: null});
              this.form.controls.rules.markAsPristine();
              this.form.controls.rules.markAsUntouched();
            }),
          ),
          of(null),
        ).pipe(
          mapTo(result),
        ),
      ),
      tap(() => close && this.matDialogRef.close(this.reload)),
    );
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
