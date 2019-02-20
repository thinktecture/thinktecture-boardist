import {HttpClientModule} from '@angular/common/http';
import {NgModule} from '@angular/core';
import {ReactiveFormsModule} from '@angular/forms';
import {
  MAT_DIALOG_DEFAULT_OPTIONS,
  MAT_FORM_FIELD_DEFAULT_OPTIONS,
  MatButtonModule,
  MatDialogModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatSelectModule,
  MatSidenavModule,
  MatTableModule,
  MatToolbarModule,
} from '@angular/material';
import {BrowserModule} from '@angular/platform-browser';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {OVERVIEW_CONTEXT} from './components/abstract-overview';
import {DetailComponent} from './components/detail/detail.component';
import {GameComponent} from './components/game/game.component';
import {GamesComponent} from './components/games/games.component';
import {OverviewComponent} from './components/overview/overview.component';
import {SpinnerComponent} from './components/spinner/spinner.component';
import {NumbersPipe} from './pipes/numbers.pipe';

@NgModule({
  declarations: [
    AppComponent,
    GamesComponent,
    GameComponent,
    SpinnerComponent,
    NumbersPipe,
    DetailComponent,
    OverviewComponent,
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
    MatProgressBarModule,
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
  ],
})
export class AppModule {
}
