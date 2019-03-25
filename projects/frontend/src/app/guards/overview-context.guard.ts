import { Inject, Injectable, Injector } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { OVERVIEW_CONTEXT, OverviewContext } from '../components/abstract-overview';
import { DetailComponent } from '../components/detail/detail.component';

@Injectable({
  providedIn: 'root',
})
export class OverviewContextGuard implements CanActivate {
  constructor(@Inject(OVERVIEW_CONTEXT) private readonly context: OverviewContext<any, any>, private readonly injector: Injector) {
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot,
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    this.context.title = route.data.title;
    this.context.service = this.injector.get(route.data.service);
    this.context.detail = route.data.detail || DetailComponent;

    return true;
  }
}
