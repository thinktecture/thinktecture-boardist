import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {GamesComponent} from './components/games/games.component';
import {PublishersComponent} from './components/publishers/publishers.component';

const routes: Routes = [
  { path: 'games', component: GamesComponent },
  { path: 'publishers', component: PublishersComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {
}
