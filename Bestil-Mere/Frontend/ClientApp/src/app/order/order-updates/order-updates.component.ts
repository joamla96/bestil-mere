import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from '@angular/router';
import {OrderService} from '../shared/order.service';
import {first, switchMap} from 'rxjs/operators';
import {OrderStatus} from '../shared/orderStatus';

@Component({
	selector: 'app-order-updates',
	templateUrl: './order-updates.component.html',
	styleUrls: ['./order-updates.component.css']
})
export class OrderUpdatesComponent implements OnInit {
	statusResps: OrderStatus[] = [];
	OrderStatus = OrderStatus;

	constructor(private route: ActivatedRoute, private service: OrderService) {
	}

	ngOnInit() {
		this.route.queryParams.pipe(first(), switchMap(qp => {
			return this.service.openConnection(qp.id);
		})).subscribe(() => {
			this.service.orderUpdates()
				.subscribe((currentStatus: OrderStatus) => {
					this.statusResps.push(currentStatus);
				});

		});
	}

}
