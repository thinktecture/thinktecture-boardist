import { Syncable } from './syncable';

export interface Item extends Syncable {
  name: string;
  boardGameGeekId?: number;
}
