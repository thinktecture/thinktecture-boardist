import {Moment} from 'moment';
import {Category} from './category';
import {Person} from './person';

export interface Game {
  id: string;
  name: string;
  minPlayers: number;
  maxPlayers: number;
  minDuration?: number;
  maxDuration?: number;
  minAge?: number;
  buyDate?: Moment;
  buyPrice?: number;
  publisherId: string;
  boardGameGeekId?: number;
  mainGameId?: string;

  categories: Category[];
  authors: Person[];
  illustrators: Person[];
}
