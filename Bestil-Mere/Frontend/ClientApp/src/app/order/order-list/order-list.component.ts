import {Component, OnInit} from '@angular/core';
import {OrderService} from '../shared/order.service';
import {first} from "rxjs/operators";
import {Order} from "../shared/order";
import {AuthService} from "../../auth.service";

@Component({
	selector: 'app-order-list',
	templateUrl: './order-list.component.html',
	styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
	orders: Order[] = [];

	constructor(private service: OrderService,
							private authService: AuthService) {
	}

	ngOnInit() {
		this.service.getOrders(this.authService.customerId)
			.pipe(first())
			.subscribe(data => {
				this.orders = data;
			});
	}

}
