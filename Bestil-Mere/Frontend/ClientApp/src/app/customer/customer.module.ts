import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerProfileComponent } from './customer-profile/customer-profile.component';
import {ReactiveFormsModule} from "@angular/forms";
import {Route, RouterModule} from "@angular/router";

const routes: Route[] = [
	{
		path: 'profile',
		component: CustomerProfileComponent
	},
];


@NgModule({
  declarations: [CustomerProfileComponent],
  imports: [
    CommonModule,
		ReactiveFormsModule,
		RouterModule.forChild(routes)
	]
})
export class CustomerModule { }
