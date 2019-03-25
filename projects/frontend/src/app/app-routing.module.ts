import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { GamesComponent } from './components/games/games.component';
import { OverviewComponent } from './components/overview/overview.component';
import { PublishersComponent } from './components/publishers/publishers.component';
import { OverviewContextGuard } from './guards/overview-context.guard';
import { CategoriesService } from './services/categories.service';
import { MechanicsService } from './services/mechanics.service';
import { PersonsService } from './services/persons.service';

const routes: Routes = [
  { path: 'games', component: GamesComponent },
  { path: 'publishers', component: PublishersComponent },
  {
    path: 'persons',
    component: OverviewComponent,
    canActivate: [OverviewContextGuard],
    data: { title: 'Persons', service: PersonsService },
  },
  {
    path: 'categories',
    component: OverviewComponent,
    canActivate: [OverviewContextGuard],
    data: { title: 'Categories', service: CategoriesService },
  },
  {
    path: 'mechanics',
    component: OverviewComponent,
    canActivate: [OverviewContextGuard],
    data: { title: 'Mechanics', service: MechanicsService },
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { scrollPositionRestoration: 'top' })],
  exports: [RouterModule],
})
export class AppRoutingModule {
}
