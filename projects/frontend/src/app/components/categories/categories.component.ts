import {ChangeDetectionStrategy, Component} from '@angular/core';
import {MatDialog} from '@angular/material';
import {Category} from '../../models/category';
import {CategoriesService} from '../../services/categories.service';
import {AbstractOverview} from '../abstract-overview';

@Component({
  selector: 'ttb-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class CategoriesComponent extends AbstractOverview<Category> {
  constructor(categories: CategoriesService, matDialog: MatDialog) {
    super('Categories', categories, matDialog);
  }
}
