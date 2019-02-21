import {Moment} from 'moment';
import {Category} from './category';
import {Item} from './item';
import {Person} from './person';

export interface Game extends Item {
  minPlayers: number;
  maxPlayers: number;
  minDuration?: number;
  maxDuration?: number;
  minAge?: number;
  buyDate?: Moment;
  buyPrice?: number;
  publisherId: string;
  mainGameId?: string;

  categories: Category[];
  authors: Person[];
  illustrators: Person[];

  hasRules: boolean;
}
