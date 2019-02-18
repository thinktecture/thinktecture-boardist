import {MediaMatcher} from '@angular/cdk/layout';
import {Component, OnDestroy, OnInit, ViewChild} from '@angular/core';
import {MatSidenav} from '@angular/material';
import {fromEvent, Subscription} from 'rxjs';

@Component({
  selector: 'ttb-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
  // tslint:disable-next-line:use-host-property-decorator
  host: {
    '[class.is-mobile]': 'mobileQuery.matches',
  },
})
export class AppComponent implements OnInit, OnDestroy {
  private subscription = Subscription.EMPTY;

  mobileQuery: MediaQueryList;
  @ViewChild(MatSidenav) snav: MatSidenav;

  constructor(private readonly media: MediaMatcher) {
  }

  ngOnInit(): void {
    this.mobileQuery = this.media.matchMedia('(max-width: 600px)');
    this.subscription = fromEvent(this.mobileQuery, 'change').subscribe();
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }
}
