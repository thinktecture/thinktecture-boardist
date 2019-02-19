import {Moment} from 'moment';
import {Category} from './category';
import {Person} from './person';

export interface Game {
  id: string;
  name: string;
  minPlayers: number;
  maxPlayers: number;
  mainGameId?: string;
}

export interface GameDetail extends Game {
  minDuration?: number;
  maxDuration?: number;
  buyDate?: Moment;
  buyPrice?: number;
  publisherId: string;
  boardGameGeekId?: number;

  categories: Category[];
  authors: Person[];
  illustrators: Person[];
}
