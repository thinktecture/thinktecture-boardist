import { Pipe, PipeTransform } from '@angular/core';
import { PublishersService } from '../services/publishers.service';

@Pipe({
  name: 'publisher$',
})
export class PublisherPipe implements PipeTransform {
  constructor(private readonly publishers: PublishersService) {
  }

  async transform(publisherId: string): Promise<string> {
    if (publisherId) {
      const publisher = await this.publishers.get(publisherId);
      if (publisher) {
        return publisher.name;
      }
    }

    return '';
  }
}
