import { ChangeDetectionStrategy, ChangeDetectorRef, Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { combineLatest, defer, Observable } from 'rxjs';
import { map, repeatWhen } from 'rxjs/operators';
import { Category } from '../../models/category';
import { Game } from '../../models/game';
import { Mechanic } from '../../models/mechanic';
import { Person } from '../../models/person';
import { Publisher } from '../../models/publisher';
import { CategoriesService } from '../../services/categories.service';
import { FileCategory, GamesService } from '../../services/games.service';
import { MechanicsService } from '../../services/mechanics.service';
import { PersonsService } from '../../services/persons.service';
import { PublishersService } from '../../services/publishers.service';
import { AbstractDetail, DetailContext } from '../abstract-detail';

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
    minAge: [null, Validators.required],
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

  private listener = () => this.changeDetectorRef.markForCheck();

  get coverSrc(): string {
    return this.context.item.id ? `url(${this.context.service.getLogoUrl(this.context.item.id)})` : '';
  }

  get rulesSrc(): string {
    return this.context.service.getRulesUrl(this.context.item.id);
  }

  constructor(
    private readonly fb: FormBuilder,
    private readonly publishers: PublishersService,
    private readonly persons: PersonsService,
    private readonly categories: CategoriesService,
    private readonly mechanics: MechanicsService,
    @Inject(MAT_DIALOG_DATA) context: DetailContext<GamesService, Game>,
    matDialogRef: MatDialogRef<GameComponent>,
    private readonly changeDetectorRef: ChangeDetectorRef,
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

  protected async afterImport(): Promise<void> {
    await super.afterImport();

    await this.publishers.clearCache();
    await this.persons.clearCache();
    await this.categories.clearCache();
    await this.mechanics.clearCache();
  }

  protected async save(close: boolean): Promise<Game> {
    const { service } = this.context;
    const result = await this.context.service.save({ ...this.form.value, id: this.context.item.id, rules: null });

    if (this.form.value.rules) {
      await this.context.service.upload(FileCategory.Rules, result.id, this.form.value.rules);

      this.context.item.hasRules = true;
      this.form.patchValue({ rules: null });
      this.form.controls.rules.markAsPristine();
      this.form.controls.rules.markAsUntouched();

      await this.context.service.update(this.context.item);
    }

    if (close) {
      this.matDialogRef.close(this.reload);
    }

    return result;
  }

  async searchForBoardGameGeekId(): Promise<void> {
    const control = this.form.controls.boardGameGeekId;

    control.disable();
    this.searching = true;

    try {
      const boardGameGeekId = await this.context.service.search(this.form.value.name);
      if (boardGameGeekId === null) {
        return;
      }

      this.form.patchValue({ boardGameGeekId });
      control.markAsDirty();
      control.markAsTouched();
    } finally {
      control.enable();
      this.searching = false;
    }
  }
}
