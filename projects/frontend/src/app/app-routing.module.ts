import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CategoriesComponent} from './components/categories/categories.component';
import {GamesComponent} from './components/games/games.component';
import {MechanicsComponent} from './components/mechanics/mechanics.component';
import {PersonsComponent} from './components/persons/persons.component';
import {PublishersComponent} from './components/publishers/publishers.component';

const routes: Routes = [
  { path: 'games', component: GamesComponent },
  { path: 'publishers', component: PublishersComponent },
  { path: 'persons', component: PersonsComponent },
  { path: 'categories', component: CategoriesComponent },
  { path: 'mechanics', component: MechanicsComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {
}
