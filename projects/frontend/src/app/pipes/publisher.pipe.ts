import { Pipe, PipeTransform } from '@angular/core';
import { PublishersService } from '../services/publishers.service';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

@Pipe({
  name: 'publisher$',
})
export class PublisherPipe implements PipeTransform {
  constructor(private readonly publishers: PublishersService) {
  }

  transform(publisherId: string): Observable<string> {
    if (publisherId) {
      return this.publishers.get(publisherId).pipe(
        map(publisher => publisher ? publisher.name : ''),
      );
    }

    return of('');
  }
}
