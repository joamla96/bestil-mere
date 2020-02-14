import {Component, OnInit} from '@angular/core';
import {OrderService} from '../shared/order.service';
import {first} from 'rxjs/operators';
import {Router} from '@angular/router';

@Component({
	selector: 'app-create-order',
	templateUrl: './create-order.component.html',
	styleUrls: ['./create-order.component.css']
})
export class CreateOrderComponent implements OnInit {

	constructor(private service: OrderService, private router: Router) {
	}

	ngOnInit() {
	}

	createTestOrder(): void {
		this.service.createTestOrder()
			.pipe(first())
			.subscribe(o => {
					this.router.navigateByUrl('/order/order-updates?id=' + o.id);
			});
	}

}
