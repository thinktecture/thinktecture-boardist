import {Moment} from 'moment';
import {Category} from './category';
import {Person} from './person';

export interface Game {
  id: string;
  name: string;
}

export interface GameDetail extends Game {
  minPlayers: number;
  maxPlayers: number;
  minDuration?: number;
  maxDuration?: number;
  perPlayerDuration?: number;
  buyDate?: Moment;
  buyPrice?: number;
  publisherId: string;
  mainGameId?: string;

  categories: Category[];
  authors: Person[];
  illustrators: Person[];
}
