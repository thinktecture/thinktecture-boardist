import { MediaMatcher } from '@angular/cdk/layout';
import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatSidenav } from '@angular/material';

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
  mobileQuery: MediaQueryList;
  @ViewChild(MatSidenav) snav: MatSidenav;

  private listener = () => undefined;

  constructor(private readonly media: MediaMatcher) {
  }

  ngOnInit(): void {
    this.mobileQuery = this.media.matchMedia('(max-width: 600px)');
    this.mobileQuery.addListener(this.listener);
  }

  ngOnDestroy(): void {
    this.mobileQuery.removeListener(this.listener);
  }
}
