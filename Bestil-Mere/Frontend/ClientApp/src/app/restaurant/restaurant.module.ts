import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {Route, RouterModule} from '@angular/router';
import { RestaurantUpdatesComponent } from './restaurant-updates/restaurant-updates.component';
import { RestaurantListComponent } from './restaurant-list/restaurant-list.component';

const routes: Route[] = [
	{
		path: 'restaurant-updates',
		component: RestaurantUpdatesComponent
	},
	{
		path: 'restaurant-list',
		component: RestaurantListComponent
	}
];

@NgModule({
	declarations: [RestaurantUpdatesComponent, RestaurantListComponent],
	imports: [
		CommonModule,
		RouterModule.forChild(routes)
	]
})
export class RestaurantModule {
}
