import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
//import {CreateOrderComponent} from './create-order/create-order.component';
import {Route, RouterModule} from '@angular/router';
import { RestaurantUpdatesComponent } from './restaurant-updates/restaurant-updates.component';
//import { OrderListComponent } from './order-list/order-list.component';

const routes: Route[] = [
//	{
//		path: '',
//		component: CreateOrderComponent
//	},
	{
		path: 'restaurant-updates',
		component: RestaurantUpdatesComponent
	},
//	{
//		path: 'order-list',
//		component: OrderListComponent
//	}
];

@NgModule({
//	declarations: [CreateOrderComponent, OrderUpdatesComponent, OrderListComponent],
	declarations: [RestaurantUpdatesComponent],
	imports: [
		CommonModule,
		RouterModule.forChild(routes)
	]
})
export class RestaurantModule {
}
