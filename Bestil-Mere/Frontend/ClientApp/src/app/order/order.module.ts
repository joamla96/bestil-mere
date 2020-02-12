import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {CreateOrderComponent} from './create-order/create-order.component';
import {Route, RouterModule} from '@angular/router';
import { OrderUpdatesComponent } from './order-updates/order-updates.component';

const routes: Route[] = [
	{
		path: '',
		component: CreateOrderComponent
	},
	{
		path: 'order-updates',
		component: OrderUpdatesComponent
	}
];

@NgModule({
	declarations: [CreateOrderComponent, OrderUpdatesComponent],
	imports: [
		CommonModule,
		RouterModule.forChild(routes)
	]
})
export class OrderModule {
}
