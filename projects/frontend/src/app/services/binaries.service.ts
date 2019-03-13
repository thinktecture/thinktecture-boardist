import { Injectable } from '@angular/core';
import { GamesService } from './games.service';
import { concatMap, delay, map, retryWhen, switchMap } from 'rxjs/operators';
import { from, Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Game } from '../models/game';

@Injectable({
  providedIn: 'root',
})
export class BinariesService {
  constructor(private readonly games: GamesService, private readonly httpClient: HttpClient) {
  }

  sync(): void {
    this.games.getAll().pipe(
      map(games => games.filter(game => !game.hasLogo)),
      switchMap(games => from(games)),
      concatMap(game => this.requestLogo(game)),
      retryWhen(errors => errors.pipe(delay(environment.syncStartDelay))),
    ).subscribe();
  }

  private requestLogo(game: Game): Observable<void> {
    return this.httpClient.get(this.games.getLogoUrl(game.id), { responseType: 'blob' }).pipe(
      switchMap(() => this.games.update({ ...game, hasLogo: true }, { emitRefresh: false })),
      delay(2000),
    );
  }
}
