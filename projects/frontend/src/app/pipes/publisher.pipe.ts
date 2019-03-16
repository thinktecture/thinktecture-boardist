import { Pipe, PipeTransform } from '@angular/core';
import { EMPTY, Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';
import { PublishersService } from '../services/publishers.service';

@Pipe({
  name: 'publisher$',
})
export class PublisherPipe implements PipeTransform {
  constructor(private readonly publishers: PublishersService) {
  }

  transform(publisherId: string): Observable<string> {
    return publisherId
      ? this.publishers.get(publisherId).pipe(
        filter(publisher => !!publisher),
        map(publisher => publisher.name),
      )
      : EMPTY;
  }
}
