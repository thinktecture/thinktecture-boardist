import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'numberOfPlayers',
})
export class NumberOfPlayersPipe implements PipeTransform {
  transform(value: any, start: number, end: number): any {
    return new Array(end - start).fill(null).map((_, index) => index + start + 1);
  }
}
