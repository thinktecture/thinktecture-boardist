import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import {
  MAT_DIALOG_DEFAULT_OPTIONS,
  MAT_FORM_FIELD_DEFAULT_OPTIONS,
  MatButtonModule,
  MatCheckboxModule,
  MatDialogModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatProgressSpinnerModule,
  MatSelectModule,
  MatSidenavModule,
  MatSortModule,
  MatTableModule,
  MatToolbarModule,
} from '@angular/material';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { OVERVIEW_CONTEXT } from './components/abstract-overview';
import { DetailComponent } from './components/detail/detail.component';
import { GameComponent } from './components/game/game.component';
import { GamesComponent } from './components/games/games.component';
import { OverviewComponent } from './components/overview/overview.component';
import { PublisherComponent } from './components/publisher/publisher.component';
import { PublishersComponent } from './components/publishers/publishers.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import {
  FileValueAccessorDirective,
  MultipleFileValueAccessorDirective,
} from './directives/file-value-accessor.directive';
import { NumbersPipe } from './pipes/numbers.pipe';
import { PublisherPipe } from './pipes/publisher.pipe';
import { ServiceWorkerModule } from '@angular/service-worker';
import { environment } from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    GamesComponent,
    GameComponent,
    SpinnerComponent,
    NumbersPipe,
    DetailComponent,
    OverviewComponent,
    PublisherComponent,
    PublisherPipe,
    PublishersComponent,
    FileValueAccessorDirective,
    MultipleFileValueAccessorDirective,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatToolbarModule,
    MatListModule,
    MatTableModule,
    MatDialogModule,
    MatProgressSpinnerModule,
    MatInputModule,
    MatSelectModule,
    MatFormFieldModule,
    MatCheckboxModule,
    MatSortModule,
    ServiceWorkerModule.register('ngsw-worker.js', { enabled: environment.production }),
  ],
  providers: [
    {
      provide: MAT_DIALOG_DEFAULT_OPTIONS,
      useValue: { closeOnNavigation: true, disableClose: true, width: '500px', maxWidth: '90vw', hasBackdrop: true, autoFocus: true },
    },
    { provide: MAT_FORM_FIELD_DEFAULT_OPTIONS, useValue: { appearance: 'fill' } },
    { provide: OVERVIEW_CONTEXT, useValue: {} },
  ],
  bootstrap: [AppComponent],
  entryComponents: [
    GameComponent,
    DetailComponent,
    PublisherComponent,
  ],
})
export class AppModule {
  constructor() {
    // Workaround. Angular ServiceWorker is only installed when the app become stable,
    // but this never happens because of used timers
    if ('serviceWorker' in navigator && environment.production) {
      navigator.serviceWorker.register('ngsw-worker.js');
    }
  }
}
