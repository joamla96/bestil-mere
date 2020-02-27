import {Component, OnInit} from '@angular/core';
import {OrderService} from '../shared/order.service';
import {first} from 'rxjs/operators';
import {Router} from '@angular/router';
import {Restaurant} from "../../restaurant/shared/restaurant";
import {RestaurantService} from "../../restaurant/shared/restaurant.service";
import {AuthService} from "../../auth.service";

@Component({
	selector: 'app-create-order',
	templateUrl: './create-order.component.html',
	styleUrls: ['./create-order.component.css']
})
export class CreateOrderComponent implements OnInit {
	restaurants: Restaurant[] = [];
	pickedCountry = 'DK';

	constructor(private service: OrderService,
							private router: Router,
							private restaurantService: RestaurantService,
							private authService: AuthService) {
	}

	ngOnInit() {
		this.restaurantService.getRestaurants()
			.pipe(first())
			.subscribe(data => {
				this.restaurants = data;
			});
	}

	createTestOrder(restaurantId): void {
		this.service.createTestOrder(restaurantId, this.pickedCountry, this.authService.customerId)
			.pipe(first())
			.subscribe(o => {
					this.router.navigateByUrl('/order/order-updates?id=' + o.id);
			});
	}

}
