import { Injectable } from '@angular/core';
import { GamesService } from './games.service';
import { concatMap, delay, map, switchMap } from 'rxjs/operators';
import { from, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class BinariesService {
  constructor(private readonly games: GamesService) {
  }

  sync(): void {
    this.games.getAll().pipe(
      map(games => games.filter(game => !game.hasLogo)),
      switchMap(games => from(games)),
      concatMap(game => this.requestLogo(game.id).pipe(
        switchMap(() => this.games.update({ ...game, hasLogo: true })),
      )),
    ).subscribe();
  }

  private requestLogo(id: string): Observable<void> {
    return new Observable<void>(subscriber => {
      const image = new Image();
      image.onload = () => {
        subscriber.next();
        subscriber.complete();
      };
      image.onerror = () => subscriber.error();
      image.src = this.games.getLogoUrl(id);
    }).pipe(
      delay(2000),
    );
  }
}
