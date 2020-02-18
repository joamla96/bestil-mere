import {Component, OnInit} from '@angular/core';
import {RestaurantService} from '../shared/restaurant.service';
import {first, switchMap} from "rxjs/operators";
import {Restaurant} from "../shared/restaurant";
import {Router} from "@angular/router";

@Component({
	selector: 'app-restaurant-list',
	templateUrl: './restaurant-list.component.html',
	styleUrls: ['./restaurant-list.component.css']
})
export class RestaurantListComponent implements OnInit {
	restaurants: Restaurant[] = [];

	constructor(private service: RestaurantService, private router: Router) {
	}

	ngOnInit() {
		this.service.getRestaurants()
			.subscribe(data => {
				this.restaurants = data;
			});
	}

	showRestaurantOrders(restaurantId) {
		this.router.navigateByUrl('/restaurant/restaurant-updates?id=' + restaurantId);
	}
}
