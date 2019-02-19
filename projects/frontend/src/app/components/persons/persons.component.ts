import {ChangeDetectionStrategy, Component} from '@angular/core';
import {MatDialog} from '@angular/material';
import {Person} from '../../models/person';
import {PersonsService} from '../../services/persons.service';
import {AbstractOverview} from '../abstract-overview';

@Component({
  selector: 'ttb-persons',
  templateUrl: './persons.component.html',
  styleUrls: ['./persons.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class PersonsComponent extends AbstractOverview<Person> {
  constructor(persons: PersonsService, matDialog: MatDialog) {
    super('Persons', persons, matDialog);
  }
}
