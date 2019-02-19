import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'numbers'
})
export class NumbersPipe implements PipeTransform {
  transform(value: any, start: number, end: number, step = 1): number[] {
    return new Array((end - start) / step + 1).fill(null).map((_, index) => index * step + start);
  }
}
