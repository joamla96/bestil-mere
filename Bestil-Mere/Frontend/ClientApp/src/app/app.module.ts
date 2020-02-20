import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {CounterComponent} from './counter/counter.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';
import {LogisticsComponent} from './logistics/logistics.component';

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		CounterComponent,
		FetchDataComponent,
		LogisticsComponent
	],
	imports: [
		BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
		HttpClientModule,
		FormsModule,
		RouterModule.forRoot([
			{path: '', component: HomeComponent, pathMatch: 'full'},
			{path: 'logistics', component: LogisticsComponent},
			{path: 'order', loadChildren: () => import('./order/order.module').then(m => m.OrderModule)},
			{path: 'restaurant', loadChildren: () => import('./restaurant/restaurant.module').then(m => m.RestaurantModule)},
			{path: 'customer', loadChildren: () => import('./customer/customer.module').then(m => m.CustomerModule)},
		])
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule {
}
