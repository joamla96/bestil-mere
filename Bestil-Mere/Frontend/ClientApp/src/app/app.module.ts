import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import {RouterModule} from '@angular/router';

import {AppComponent} from './app.component';
import {NavMenuComponent} from './nav-menu/nav-menu.component';
import {HomeComponent} from './home/home.component';
import {CounterComponent} from './counter/counter.component';
import {FetchDataComponent} from './fetch-data/fetch-data.component';
import {LogisticsComponent} from './logistics/logistics.component';
import {JwtInterceptor} from "./jwt.interceptor";
import {LoginComponent} from "./login/login.component";
import {AuthGuard} from "./shared/auth.guard";

@NgModule({
	declarations: [
		AppComponent,
		NavMenuComponent,
		HomeComponent,
		CounterComponent,
		FetchDataComponent,
		LogisticsComponent,
		LoginComponent
	],
	imports: [
		BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
		HttpClientModule,
		FormsModule,
		RouterModule.forRoot([
			{path: '', component: HomeComponent, pathMatch: 'full'},
			{path: 'logistics', component: LogisticsComponent},
			{path: 'order', canActivate: [AuthGuard], loadChildren: () => import('./order/order.module').then(m => m.OrderModule)},
			{path: 'restaurant', loadChildren: () => import('./restaurant/restaurant.module').then(m => m.RestaurantModule)},
			{path: 'customer', canActivate: [AuthGuard], loadChildren: () => import('./customer/customer.module').then(m => m.CustomerModule)},
			{path: 'login', component: LoginComponent}
		]),
		ReactiveFormsModule
	],
	providers: [
		{
			provide: HTTP_INTERCEPTORS,
			useClass: JwtInterceptor,
			multi: true
		}
	],
	bootstrap: [AppComponent]
})
export class AppModule {
}
