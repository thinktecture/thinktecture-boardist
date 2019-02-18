import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'durations'
})
export class DurationsPipe implements PipeTransform {
  transform(value: any, start: number, end: number, step = 5): any {
    return new Array((end - start) / step).fill(null).map((_, index) => index * step + start);
  }
}
